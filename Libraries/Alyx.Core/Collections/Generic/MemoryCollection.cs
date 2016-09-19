using System;

namespace Alyx.Core.Collections.Generic
{
	public class MemoryCollection<T>
	{
		/*
		 * First developed by Atkinson and Shiffrin (1968), and refined by others, including Raajimakers and Shiffrin,
		 * the Dual-store Memory Search model, now referred to as SAM or search of associative memory model, remains as
		 * one of the most influential computational models of memory. The model uses both short-term memory, termed
		 * short-term store (STS), and long-term memory, termed long-term store (LTS) or episodic matrix, in its
		 * mechanism.
		 * 
		 * When an item is first encoded, it is introduced into the short-term store. While the item stays in the
		 * short-term store, vector representations in long-term store go through a variety of associations. Items
		 * introduced in short-term store go through three different types of association: (autoassociation ) the
		 * self-association in long-term store, (heteroassociation ) the inter-item association in long-term store, and
		 * the (context association ) which refers to association between the item and its encoded context. For each
		 * item in short-term store, the longer the duration of time an item resides within the short-term store, the
		 * greater its association with itself will be with other items that co-reside within short-term store, and
		 * with its encoded context.
		 * 
		 * The size of the short-term store is defined by a parameter, r. As an item is introduced into the short-term
		 * store, and if the short-term store has already been occupied by a maximum number of items, the item will
		 * probably drop out of the short-term storage.
		 * 
		 * As items co-reside in the short-term store, their associations are constantly being updated in the long-term
		 * store matrix. The strength of association between two items depends on the amount of time the two memory
		 * items spend together within the short-term store, known as the contiguity effect. Two items that are
		 * contiguous have greater associative strength and are often recalled together from long-term storage.
		 * 
		 * Furthermore, the primacy effect, an effect seen in memory recall paradigm, reveals that the first few items
		 * in a list have a greater chance of being recalled over others in the STS, while older items have a greater
		 * chance of dropping out of STS. The item that managed to stay in the STS for an extended amount of time would
		 * have formed a stronger autoassociation, heteroassociation and context association than others, ultimately
		 * leading to greater associative strength and a higher chance of being recalled.
		 * 
		 * The recency effect of recall experiments is when the last few items in a list are recalled exceptionally
		 * well over other items, and can be explained by the short-term store. When the study of a given list of
		 * memory has been finished, what resides in the short-term store in the end is likely to be the last few
		 * items that were introduced last. Because the short-term store is readily accessible, such items would be
		 * recalled before any item stored within long-term store. This recall accessibility also explains the fragile
		 * nature of recency effect, which is that the simplest distractors can cause a person to forget the last few
		 * items in the list, as the last items would not have had enough time to form any meaningful association
		 * within the long-term store. If the information is dropped out of the short-term store by distractors, the
		 * probability of the last items being recalled would be expected to be lower than even the pre-recency items
		 * in the middle of the list.
		 * 
		 * The dual-store SAM model also utilizes memory storage, which itself can be classified as a type of long-term
		 * storage: the semantic matrix. The long-term store in SAM represents the episodic memory, which only deals
		 * with new associations that were formed during the study of an experimental list; pre-existing associations
		 * between items of the list, then, need to be represented on different matrix, the semantic matrix. The
		 * semantic matrix remains as another source of information that is not modified by episodic associations that
		 * are formed during the exam.
		 * 
		 * Thus, the two types of memory storage, short- and long-term stores, are used in the SAM model. In the recall
		 * process, items residing in short-term memory store will be recalled first, followed by items residing in
		 * long-term store, where the probability of being recalled is proportional to the strength of the association
		 * present within the long-term store. Another memory storage, the semantic matrix, is used to explain the
		 * semantic effect associated with memory recall.
		 * 
		 */

		private class ShortTermMemoryStack : System.Collections.Generic.IEnumerable<T>
		{
			private System.Collections.Generic.Stack<T> _stack = new System.Collections.Generic.Stack<T>();

			private int mvarCapacity = 10;
			public int Capacity { get { return mvarCapacity; } set { mvarCapacity = value; } }
			
			public void Push(T item)
			{
				if (_stack.Count == mvarCapacity) {
					// we've reached capacity, so pop one off the stack and discard it
					Pop ();
				}
				_stack.Push (item);
			}
			public T Pop()
			{
				return _stack.Pop();
			}

			#region IEnumerable implementation

			public System.Collections.Generic.IEnumerator<T> GetEnumerator ()
			{
				throw new NotImplementedException ();
			}

			#endregion

			#region IEnumerable implementation

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
			{
				throw new NotImplementedException ();
			}

			#endregion
		}

		private ShortTermMemoryStack sts = new ShortTermMemoryStack();

		public void Add(T item)
		{

			sts.Push (item);
		}

		public void Process()
		{
		}
	}
}

