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
                new CommandParser(),
                string.Empty);
        }

        private VMTranslator CreateSutWithStaticVariable(string variableName)
        {
            return new VMTranslator(
                new TextCleaner(),
                new CommandParser(),
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
                $"// pop static 4",
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
    }
}
