#!/bin/bash

OUT_FOLDER=mono

rm $OUT_FOLDER/*
find VMTranslator -type f | grep -i .cs$ | grep -iv .assemblyinfo.cs | xargs -i cp {} $OUT_FOLDER/
find VMTranslator.Lib -type f | grep -i .cs$ | grep -iv .assemblyinfo.cs | xargs -i cp {} $OUT_FOLDER/

mv $OUT_FOLDER/VMTranslator.cs $OUT_FOLDER/VMTranslator.Lib.cs
mv $OUT_FOLDER/Program.cs $OUT_FOLDER/VMTranslator.cs

echo c# debug > $OUT_FOLDER/lang.txt

cd $OUT_FOLDER
zip mono.zip *
cd ..