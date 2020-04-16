# VM Translator Requirements

## High Level Requirements
- Read a VM code file
- Translate it to assembly
- Write the translation to an assembly file

## Requirements of the translator
- Copy the original VM code as a comment
- Ignore whitespace and comments
- Handle push and pop commands
- Handle 8 memory segments
  - local
  - argument
  - this
  - that
  - constant
  - static
  - pointer
  - temp


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