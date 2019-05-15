# Test Project

## Problem

StartWord - a four letter word (that you can assume is found in the DictionaryFile file)

EndWord - a four letter word (that you can assume is found in the DictionaryFile file)

ResultFile - the file name of a text file that will contain the result

The result is the shortest list of four letter words, starting with StartWord, and ending with EndWord, with a number of intermediate words that must appear in the DictionaryFile file where each word differs from the previous word by one letter only.

For example, if StartWord = Spin, EndWord = Spot and DictionaryFile file contains

Spin

Spit

Spat

Spot

Span

then ResultFile should contain

Spin

Spit

Spot

## Structure 
```
BluePrisim.Services - Library

BluePrisim.Test - Xunit Tests

BluePrisim.App - Console Application
```
### Tech Stack

netcoreapp2.1
Moq
FluentAssertions
xunit


