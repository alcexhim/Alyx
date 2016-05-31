#!/bin/bash

for f in `find /usr/share/alyx | grep \~`
do
	echo $f
	rm $f
done

