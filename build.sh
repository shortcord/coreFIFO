#! /bin/bash

gcc -Wall -shared -o libPipes.so -fPIC lib.c
gcc main.c -o Hi ./libPipes.so
