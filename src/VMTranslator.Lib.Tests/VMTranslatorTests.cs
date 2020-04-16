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
                new CommandParser());
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

            Assert.Equal(0, result.Length);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_IgnoresComments()
        {
            var lines = new []
            {
                "// Ignore this"
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(0, result.Length);
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

        [Fact]
        public void TranslateVMcodeToAssembly_TranslatesPushLocal5()
        {
            var test = "push local 5";
            var lines = new [] { test };
            var expected = new []
            {
                "// push local 5",
                "@5",
                "D=A",
                "@LCL",
                "A=M+D",
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
        public void TranslateVMcodeToAssembly_TranslatesPushLocal0()
        {
            var test = "push local 0";
            var lines = new [] { test };
            var expected = new []
            {
                "// push local 0",
                "@0",
                "D=A",
                "@LCL",
                "A=M+D",
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
        public void TranslateVMcodeToAssembly_TranslatesPullLocal5()
        {
            var test = "pull local 5";
            var lines = new [] { test };
            var expected = new []
            {
                "// pull local 5",
                "@SP",
                "M=M-1",
                "A=M",
                "D=M",
                "@5",
                "D=A",
                "@LCL",
                "A=M+D",
                "M=D"
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }
    }
}
