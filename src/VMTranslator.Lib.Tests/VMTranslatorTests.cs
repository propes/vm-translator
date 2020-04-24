using System;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class VMTranslatorTests
    {
        private VMTranslator CreateSut()
        {
            return new VMTranslator(
                new TextCleaner(),
                new CommandParser(
                    new ArithmeticCommandParser(),
                    new StackOperationCommandParser()),
                string.Empty);
        }

        private VMTranslator CreateSutWithStaticVariable(string variableName)
        {
            return new VMTranslator(
                new TextCleaner(),
                new CommandParser(
                    new ArithmeticCommandParser(),
                    new StackOperationCommandParser()
                ),
                variableName);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_IgnoresWhitespace()
        {
            var lines = new string[]
            {
                "  ",
                "\n",
                "\t"
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Empty(result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_IgnoresComments()
        {
            var lines = new []
            {
                "// Ignore this"
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Empty(result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_CopiesOriginalCodeAsComment()
        {
            var lines = new []
            {
                "push local 5"
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal("// push local 5", result[0]);
        }

        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void TranslateVMcodeToAssembly_TranslatesPushLocal5(string segment, string code)
        {
            var test = $"push {segment} 5";
            var lines = new [] { test };
            var expected = new []
            {
                $"// push {segment} 5",
                $"@{code}",
                "D=M",
                "@5",
                "A=D+A",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void TranslateVMcodeToAssembly_TranslatesPushLocal0(string segment, string code)
        {
            var test = $"push {segment} 0";
            var lines = new [] { test };
            var expected = new []
            {
                $"// push {segment} 0",
                $"@{code}",
                "D=M",
                "@0",
                "A=D+A",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void TranslateVMcodeToAssembly_TranslatesPopLocal5(string segment, string code)
        {
            var test = $"pop {segment} 5";
            var lines = new [] { test };
            var expected = new []
            {
                $"// pop {segment} 5",
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{code}",
                "A=M",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "M=D",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void TranslateVMcodeToAssembly_TranslatesPopLocal2(string segment, string code)
        {
            var test = $"pop {segment} 2";
            var lines = new [] { test };
            var expected = new []
            {
                $"// pop {segment} 2",
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{code}",
                "A=M",
                "A=A+1",
                "A=A+1",
                "M=D",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void TranslateVMcodeToAssembly_TranslatesPopLocal0(string segment, string code)
        {
            var test = $"pop {segment} 0";
            var lines = new [] { test };
            var expected = new []
            {
                $"// pop {segment} 0",
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{code}",
                "A=M",
                "M=D",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesPushConstant5()
        {
            var test = $"push constant 7";
            var lines = new [] { test };
            var expected = new []
            {
                "// push constant 7",
                "@7",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesPushStatic4()
        {
            var test = $"push static 4";
            var lines = new [] { test };
            var expected = new []
            {
                "// push static 4",
                "@Foo.4",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = CreateSutWithStaticVariable("Foo").TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesPopStatic4()
        {
            var test = $"pop static 4";
            var lines = new [] { test };
            var expected = new []
            {
                "// pop static 4",
                "@SP",
                "AM=M-1",
                "D=M",
                "@Foo.4",
                "M=D",
                ""
            };

            var result = CreateSutWithStaticVariable("Foo").TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesAdd()
        {
            var lines = new [] { "add" };
            var expected = new []
            {
                "// add",
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "M=D+M",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesSub()
        {
            var lines = new [] { "sub" };
            var expected = new []
            {
                "// sub",
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "M=M-D",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesNeg()
        {
            var lines = new [] { "neg" };
            var expected = new []
            {
                "// neg",
                "@SP",
                "A=M-1",
                "M=-M",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesEq()
        {
            var lines = new [] { "eq" };
            var expected = new []
            {
                "// eq",
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "D=M-D",
                "@EQ_0",
                "D;JEQ",
                "@SP",
                "A=M-1",
                "M=0",
                "@EQ_END_0",
                "0;JMP",
                "(EQ_0)",
                "@SP",
                "A=M-1",
                "M=-1",
                "(EQ_END_0)",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesGt()
        {
            var lines = new [] { "gt" };
            var expected = new []
            {
                "// gt",
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "D=M-D",
                "@GT_0",
                "D;JGT",
                "@SP",
                "A=M-1",
                "M=0",
                "@GT_END_0",
                "0;JMP",
                "(GT_0)",
                "@SP",
                "A=M-1",
                "M=-1",
                "(GT_END_0)",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesLt()
        {
            var lines = new [] { "lt" };
            var expected = new []
            {
                "// lt",
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "D=M-D",
                "@LT_0",
                "D;JLT",
                "@SP",
                "A=M-1",
                "M=0",
                "@LT_END_0",
                "0;JMP",
                "(LT_0)",
                "@SP",
                "A=M-1",
                "M=-1",
                "(LT_END_0)",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesAnd()
        {
            var lines = new [] { "and" };
            var expected = new []
            {
                "// and",
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "M=D&M",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesOr()
        {
            var lines = new [] { "or" };
            var expected = new []
            {
                "// or",
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "M=D|M",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesNot()
        {
            var lines = new [] { "not" };
            var expected = new []
            {
                "// not",
                "@SP",
                "A=M-1",
                "M=!M",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesPushTemp3()
        {
            var test = $"push temp 3";
            var lines = new [] { test };
            var expected = new []
            {
                "// push temp 3",
                "@R5",
                "D=A",
                "@3",
                "A=D+A",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesPopTemp5()
        {
            var test = $"pop temp 5";
            var lines = new [] { test };
            var expected = new []
            {
                "// pop temp 5",
                "@SP",
                "AM=M-1",
                "D=M",
                "@R5",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "M=D",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesPushPointer0()
        {
            var test = $"push pointer 0";
            var lines = new [] { test };
            var expected = new []
            {
                "// push pointer 0",
                "@THIS",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesPushPointer1()
        {
            var test = $"push pointer 1";
            var lines = new [] { test };
            var expected = new []
            {
                "// push pointer 1",
                "@THAT",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesPopPointer0()
        {
            var test = $"pop pointer 0";
            var lines = new [] { test };
            var expected = new []
            {
                "// pop pointer 0",
                "@SP",
                "AM=M-1",
                "D=M",
                "@THIS",
                "M=D",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesPopPointer1()
        {
            var test = $"pop pointer 1";
            var lines = new [] { test };
            var expected = new []
            {
                "// pop pointer 1",
                "@SP",
                "AM=M-1",
                "D=M",
                "@THAT",
                "M=D",
                ""
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }
    }
}
