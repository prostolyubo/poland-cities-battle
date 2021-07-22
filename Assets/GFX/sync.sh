#!/bin/bash
TMPFILE=`mktemp`
PWD=`pwd`
wget "https://www.dropbox.com/sh/xctvo97hrcmrqvk/AAC62qpO9VKnlFvcWP-4FsDGa?dl=1" -O $TMPFILE
unzip -d $PWD $TMPFILE
rm $TMPFILE