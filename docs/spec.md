# Usage

Translating a file:
`VMTranslator fileName.vm`

Translating a directory:
`VMTranslator directoryName`


# Program specification

## Input
- fileName.vm or
- directoryName (*.vm)

## Output
- fileName.asm or
- directoryName.asm


# Programming conventions
One file in any VM program is expected to be named Main.vm and one function in this file is expected to be named main.


# Implementation convention
When the VM implementation starts running, or is reset, it starts executing the argument-less OS function Sys.init.

Sys.init then calls Main.main and enters an infinite loop.


# Bootstrapping
The following code must be put in the Hack ROM at address 0:

    SP=256
    Call Sys.init


# VM language features
- primitive operators (fixed): add, sub, ...
- abstract operators (extensible): multiply, sqrt, ...

# Commands

## Arithmetic/logical commands


## Memory segment commands

### (1-4) Local, Argument, This, That memory segments
#### Push
VM: push _segment_ i
pseudo: addr=_segmentPointer_+i, *SP=*addr, SP++

#### Pop
VM:	pop _segment_ i
pseudo:	addr=_segmentPointer_+i, SP--, *addr=*SP

where segmentPointer is:
local = LCL
argument = ARG
this = THIS
that = THAT


### (5) Constant memory location
#### Push
VM:	push constant i
pseudo:	*SP=i, SP++

#### Pop
No pop


### (6) Static memory location
Create local variable 


### (7) Pointer memory location
#### Push
VM:	push pointer 0/1
pseudo:	*SP=THIS/THAT, SP++

#### Pop
VM:	pop pointer 0/1
pseudo:	SP--, THIS/THAT=*SP


### (8) Temp memory location
Uses RAM locations 5-12.

#### Push
VM: push temp i
pseudo: addr = 5 + i, *SP = *addr, SP++

#### Pop
VM: pop temp i
pseudo: addr = 5 + i, SP--, *addr = *SP


## Branching commands

### Specification
- goto *label*
- if-goto *label*
- label *label*

### Implementation

goto *label*
- jump to execute the command just after the label

if-goto *label*
- *cond* = pop
- if *cond* jump to execute the command just after the label

label *label*
- label declaration command
- see convention below


## Function commands

### Specification
- call *functionName nArgs*
- function *function nVars*
- return

### Implementation

call *functionName nArgs*
- push return address and caller's LCL, ARG, THIS, THAT onto stack
  - push *returnAddress* (using label declared below)
  - push LCL
  - push ARG
  - push THIS
  - push THAT
- reposition ARG
  - ARG = SP-5-*nArgs*
- reposition LCL
  - LCL = SP
- jump to execute called function
  - goto *functionName*
- add label: (*returnAddress*)

function *functionName nVars*
- add label: (*functionName*)
- repeat *nVars* times:
  - push 0

return
- cache end frame and return addresses in temp variables
  - endFrame = LCL
  - retAddr = *(endFrame-5)
- copy the return value to argument 0
  - *ARG = pop()
- set the SP for the caller
  - SP = ARG+1
- restore the segment pointers of the caller
  - THAT = *(endFrame-1)
  - THIS = *(endFrame-2)
  - ARG = *(endFrame-3)
  - LCL = *(endFrame-4)
- jump to the return address in the caller's code
  - goto *retAddr*


### Function labelling conventions

#### *functionName$label*
Each label in within a function should be translated as Xxx.foo$bar

Where 
- Xxx is the VM filename
- foo is the function name
- bar is the label

#### *functionName*
Each *function* command should generate a label of the format Xxx.foo

Where
- Xxx is the filename
- foo is the function name

#### *functionName$ret.i*
Each function *call* command should generate a symbol Xxx.foo$ret.i

Where
- Xxx is the filename
- foo is the function name
- i is a running integer


# Testing

## Test order
ProgramFlow
- BasicLoop
  - tests:
    - label
    - if-goto
- FibonacciSeries
 - tests:
   - same as BasicLoop
   - goto

FunctionCalls
- SimpleFunction
  - tests:
    - function
    - return
- NestedCall
  - tests:
    - 
- FibonacciElement
  - tests:
    - same as BasicFlow
    - same as NestedCall
    - recursion
    - multiple vm files
    - bootstrap code
    - initialisation of memory segments
- StaticsTest
  - tests:
    - static variable naming across different files