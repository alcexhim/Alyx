using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Alyx
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();

			InitializeControlPanels();
			SwitchPanel("ApplicationsChat");
		}

		private void InitializeControlPanels()
		{
			ControlPanel[] cps = GetAvailableControlPanels();
			foreach (ControlPanel cp in cps)
			{
				CreateTreeNodeForControlPanel(cp);

				cp.Dock = DockStyle.Fill;
				cp.Enabled = false;
				cp.Visible = false;

				splitContainer1.Panel2.Controls.Add(cp);
				cp.Name = GetControlPanelNameFromPath(cp.Path);
			}
		}

		private string GetControlPanelNameFromPath(string[] path)
		{
			return String.Join(String.Empty, path);
		}

		private void CreateTreeNodeForControlPanel(ControlPanel cp)
		{
			TreeNode tnParent = null;
			if (cp.Path == null) return;

			for (int i = 0; i < cp.Path.Length; i++)
			{
				if (tnParent == null)
				{
					if (!tv.Nodes.ContainsKey(cp.Path[i]))
					{
						tnParent = tv.Nodes.Add(cp.Path[i], cp.Path[i]);
					}
					else
					{
						tnParent = tv.Nodes[cp.Path[i]];
					}
				}
				else
				{
					if (!tnParent.Nodes.ContainsKey(cp.Path[i]))
					{
						tnParent = tnParent.Nodes.Add(cp.Path[i], cp.Path[i]);
					}
					else
					{
						tnParent = tnParent.Nodes[cp.Path[i]];
					}
				}
			}

			if (tnParent == null) return;
			tnParent.Tag = cp;
		}

		private System.Reflection.Assembly[] mvarAvailableAssemblies = null;
		private System.Reflection.Assembly[] GetAvailableAssemblies()
		{
			if (mvarAvailableAssemblies == null)
			{
				List<System.Reflection.Assembly> list = new List<System.Reflection.Assembly>();
				string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

				string[] filenames = GetAvailableFiles(path, new string[] { "*.dll", "*.exe" }, System.IO.SearchOption.AllDirectories);
				foreach (string filename in filenames)
				{
					System.Reflection.Assembly asm = null;
					try
					{
						asm = System.Reflection.Assembly.LoadFile(filename);
					}
					catch
					{

					}
					if (asm != null) list.Add(asm);
				}
				mvarAvailableAssemblies = list.ToArray();
			}
			return mvarAvailableAssemblies;
		}

		private string[] GetAvailableFiles(string path, string[] allowedExtensions, System.IO.SearchOption searchOption)
		{
			string[] array = new string[0];
			foreach (string allowedExtension in allowedExtensions)
			{
				string[] ret = System.IO.Directory.GetFiles(path, allowedExtension, searchOption);
				Array.Resize<string>(ref array, array.Length + ret.Length);
				Array.Copy(ret, 0, array, array.Length - ret.Length, ret.Length);
			}
			return array;
		}

		private Type[] mvarAvailableTypes = null;
		private Type[] GetAvailableTypes(Type[] inheritsFromTypes = null)
		{
			if (mvarAvailableTypes == null)
			{
				List<Type> list = new List<Type>();
				System.Reflection.Assembly[] asms = GetAvailableAssemblies();
				foreach (System.Reflection.Assembly asm in asms)
				{
					Type[] types = null;
					try
					{
						types = asm.GetTypes();
					}
					catch (System.Reflection.ReflectionTypeLoadException ex)
					{
						types = ex.Types;
					}

					foreach (Type type in types)
					{
						if (type != null) list.Add(type);
					}
				}
				mvarAvailableTypes = list.ToArray();
			}
			if (inheritsFromTypes == null) return mvarAvailableTypes;

			List<Type> list2 = new List<Type>();
			foreach (Type type in mvarAvailableTypes)
			{
				foreach (Type inheritsFromType in inheritsFromTypes)
				{
					if (!type.IsAbstract && type.IsSubclassOf(inheritsFromType))
					{
						list2.Add(type);
						break;
					}
				}
			}
			return list2.ToArray();
		}

		private ControlPanel[] GetAvailableControlPanels()
		{
			Type[] types = GetAvailableTypes(new Type[] { typeof(ControlPanel) });

			List<ControlPanel> list = new List<ControlPanel>();
			foreach (Type type in types)
			{
				ControlPanel cp = (ControlPanel)type.Assembly.CreateInstance(type.FullName);
				list.Add(cp);
			}
			return list.ToArray();
		}

		private void mnuFileClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void mnuFileExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			SwitchPanel(e.Node.Name);
		}

		private bool inhibitSwitchPanel = false;
		private void SwitchPanel(string name)
		{
			if (inhibitSwitchPanel) return;

			if (tv.SelectedNode == null) return;

			foreach (Control ctl in splitContainer1.Panel2.Controls)
			{
				if (ctl is ControlPanel && ctl == tv.SelectedNode.Tag)
				{
					ctl.Enabled = true;
					ctl.Visible = true;
				}
				else
				{
					ctl.Visible = false;
					ctl.Enabled = false;
				}
			}

			SwitchTreeNode(name);
		}

		private void SwitchTreeNode(string name, TreeNode parent = null)
		{
			if (parent == null)
			{
				inhibitSwitchPanel = true;
				foreach (TreeNode tn in tv.Nodes)
				{
					if (tn.Name == "node" + name)
					{
						tv.SelectedNode = tn;
						break;
					}
					else
					{
						SwitchTreeNode(name, tn);
					}
				}
				inhibitSwitchPanel = false;
			}
			else
			{
				foreach (TreeNode tn in parent.Nodes)
				{
					if (tn.Name == "node" + name)
					{
						tv.SelectedNode = tn;
						return;
					}
					else
					{
						SwitchTreeNode(name, tn);
					}
				}
			}
		}
	}
}
