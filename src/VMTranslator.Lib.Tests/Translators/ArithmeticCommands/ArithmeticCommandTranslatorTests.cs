using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class ArithmeticCommandTranslatorTests
    {
        [Fact]
        public void ToAssembly_GivenAdd_ReturnsTranslatedAssembly()
        {
            var expected = new string []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "M=D+M"
            };

            var result = new ArithmeticCommandTranslatorBuilder()
                .CreateSut()
                .ToAssembly("add");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToAssembly_GivenSub_ReturnsTranslatedAssembly()
        {
            var expected = new string []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "M=M-D"
            };

            var result = new ArithmeticCommandTranslatorBuilder()
                .CreateSut()
                .ToAssembly("sub");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToAssembly_GivenNeg_ReturnsTranslatedAssembly()
        {
            var expected = new string []
            {
                "@SP",
                "A=M-1",
                "M=-M"
            };

            var result = new ArithmeticCommandTranslatorBuilder()
                .CreateSut()
                .ToAssembly("neg");

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ToAssembly_GivenEq_ReturnsTranslatedAssembly(int eqCount)
        {
            var expected = new string []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "D=M-D",
                $"@EQ_{eqCount}",
                "D;JEQ",
                "@SP",
                "A=M-1",
                "M=0",
                $"@EQ_END_{eqCount}",
                "0;JMP",
                $"(EQ_{eqCount})",
                "@SP",
                "A=M-1",
                "M=-1",
                $"(EQ_END_{eqCount})"
            };

            var result = new ArithmeticCommandTranslatorBuilder()
                .WithEqCount(eqCount)
                .CreateSut()
                .ToAssembly("eq");

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ToAssembly_GivenGt_ReturnsTranslatedAssembly(int gtCount)
        {
            var expected = new string []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "D=M-D",
                $"@GT_{gtCount}",
                "D;JGT",
                "@SP",
                "A=M-1",
                "M=0",
                $"@GT_END_{gtCount}",
                "0;JMP",
                $"(GT_{gtCount})",
                "@SP",
                "A=M-1",
                "M=-1",
                $"(GT_END_{gtCount})"
            };

            var result = new ArithmeticCommandTranslatorBuilder()
                .WithGtCount(gtCount)
                .CreateSut()
                .ToAssembly("gt");

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ToAssembly_GivenLt_ReturnsTranslatedAssembly(int ltCount)
        {
            var expected = new string []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "D=M-D",
                $"@LT_{ltCount}",
                "D;JLT",
                "@SP",
                "A=M-1",
                "M=0",
                $"@LT_END_{ltCount}",
                "0;JMP",
                $"(LT_{ltCount})",
                "@SP",
                "A=M-1",
                "M=-1",
                $"(LT_END_{ltCount})",
            };

            var result = new ArithmeticCommandTranslatorBuilder()
                .WithLtCount(ltCount)
                .CreateSut()
                .ToAssembly("lt");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToAssembly_GivenAnd_ReturnsTranslatedAssembly()
        {
            var expected = new string []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "M=D&M"
            };

            var result = new ArithmeticCommandTranslatorBuilder()
                .CreateSut()
                .ToAssembly("and");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToAssembly_GivenOr_ReturnsTranslatedAssembly()
        {
            var expected = new string []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "M=D|M"
            };

            var result = new ArithmeticCommandTranslatorBuilder()
                .CreateSut()
                .ToAssembly("or");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToAssembly_GivenNot_ReturnsTranslatedAssembly()
        {
            var expected = new string []
            {
                "@SP",
                "A=M-1",
                "M=!M"
            };

            var result = new ArithmeticCommandTranslatorBuilder()
                .CreateSut()
                .ToAssembly("not");

            Assert.Equal(expected, result);
        }
    }
}