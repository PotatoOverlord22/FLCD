# Documentation for Scanner and Symbol Table Implementation

## Overview

This project implements a **Scanner** (lexical analyzer) for a custom programming language, as part of a mini compiler. The scanner reads the source code and identifies tokens, categorizes them as keywords, identifiers, constants, operators, or separators, and builds a **Program Internal Form (PIF)** and a **Symbol Table (ST)**. The scanner processes input files containing source code and predefined tokens, performing lexical analysis to ensure syntactic correctness or to report errors if invalid tokens are encountered.

## Components

### 1. **Symbol Table (SymbolTable Class)**

The **Symbol Table (ST)** is a hash-based data structure that stores identifiers and constants found in the source code. Each unique symbol is hashed into a specific index to optimize retrieval, insertion, and lookup. Each entry is represented by a **SymbolEntry** object containing the symbol's value and type (either identifier or constant).

- **Key Methods:**
  - `InsertAndGetPosition`: Adds a symbol to the ST if it does not already exist and returns its position as a tuple `(bucketIndex, itemIndexInBucket)`.
  - `GetPosition`: Retrieves the position of a symbol if it exists in the ST, otherwise returns `(-1, -1)`.
  - `GetEntries`: Retrieves the entire set of entries for display and debugging.
  - `GetHash`: Computes the hash value for a given symbol to determine its storage location in the ST array.

The `ST.out` file is generated by the scanner to display all the contents of the Symbol Table, providing a clear view of each bucket and its contents.

### 2. **Scanner (Scanner Class)**

The **Scanner** performs lexical analysis on source files, breaking each line of code into individual tokens and categorizing them. It references a predefined list of keywords, operators, and separators provided in the `token.in` file. 

- **Key Methods:**
  - `LoadTokens`: Reads tokens from `token.in` and classifies each as a keyword, operator, or separator.
  - `ScanSourceFile`: Analyzes the source file line by line, tokenizing and categorizing each token. It writes results to `PIF.out` and `ST.out`, and reports lexical errors with line and token information.
  - `Tokenize`: Uses regular expressions to split each line of code into individual tokens.
  - `IsIdentifier`: Validates if a token is a correctly formatted identifier.
  - `IsIntegerConstant`: Validates if a token represents a valid integer constant.

### 3. **Program Internal Form (PIF)**

The **Program Internal Form (PIF)** is a data structure that stores the sequence of tokens in the order they appear in the source file. Each entry consists of the token name and its position:
   - Keywords, operators, and separators are stored with a position of `-1`.
   - Identifiers and constants store the position as retrieved from the Symbol Table.

### 4. **Error Handling and Output Files**

The scanner outputs three files:
   - **PIF.out**: Contains each token and its associated position in the Symbol Table (or `-1` for non-identifiers).
   - **ST.out**: Displays the contents of the Symbol Table.
   - Console output: Reports any lexical errors, specifying the line and token where the error occurred. If no errors are found, it displays "No lexical errors found".

## Execution

1. **Token Load**: The scanner first loads all tokens from `token.in`.
2. **Source File Processing**: The scanner processes the source file (e.g., `p1.drgs`) line by line, categorizing tokens and recording positions in the PIF and Symbol Table.
3. **Error Checking**: For each unrecognized token, an error message is printed with the line number and token.
4. **Output Generation**: If the source file is lexically correct, "No lexical errors found" is printed; otherwise, the error details are outputted.

## Deliverables

1. **Input**: Source files (`p1`, `p2`, `p3`, `p1err`) and token definition file (`token.in`).
2. **Output**:
   - **PIF.out**: Lists each token and its position in the Symbol Table or `-1`.
   - **ST.out**: Shows Symbol Table contents with bucket indices and entries.
   - **Console Output**: Displays "No lexical errors found" if the analysis is successful or details of errors if any occur.
3. **Source Code**: All classes and methods for the scanner and symbol table.
4. **Documentation**: Describes the structure, logic, and expected results for each component.

This documentation explains the system�s purpose and structure, guiding the user through the process of setting up, running, and understanding the scanner's output.