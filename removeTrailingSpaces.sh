#!/bin/sh
find ./DiceExpressions -type f -name *.cs -o -name *.xaml -exec sed -i -e's/[[:space:]]*$//' {} +
find ./Antlr4DensityParser -type f -name *.g4 -exec sed -i -e's/[[:space:]]*$//' {} +
