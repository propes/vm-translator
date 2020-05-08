using System;
using System.IO;
using Moq;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class VMFileTranslatorTests : IDisposable
    {
        private readonly string inputFilename = "in.vm";
        private readonly string outputFilename = "out.asm";
        private readonly StreamWriter outputStream;

        public VMFileTranslatorTests()
        {
            outputStream = File.AppendText(outputFilename);
        }

        [Fact]
        public void AppendTranslationToFile_GivenNewOutputFile_AppendsTranslatedLines()
        {
            var mockVMCommands = new []
            {
                "mock",
                "vm",
                "commands"
            };

            var expectedTranslation = new []
            {
                "expected",
                "output"
            };

            var mockVMTranslator = new Mock<IVMTranslator>();
            mockVMTranslator
                .Setup(t => t.TranslateVMcodeToAssembly(mockVMCommands))
                .Returns(expectedTranslation);

            File.WriteAllLines(inputFilename, mockVMCommands);

            new VMFileTranslator(mockVMTranslator.Object).TranslateFileToStream(inputFilename, outputStream);

            outputStream.Close();
            var actualTranslation = File.ReadAllLines(outputFilename);

            Assert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void AppendTranslationToFile_GivenExistingOutputFile_AppendsTranslatedLines()
        {
            var mockVMCommands = new []
            {
                "mock",
                "vm",
                "commands"
            };

            var expectedTranslation = new []
            {
                "appended output"
            };

            var expectedOutput = new []
            {
                "existing output",
                "appended output"
            };

            var mockVMTranslator = new Mock<IVMTranslator>();
            mockVMTranslator
                .Setup(t => t.TranslateVMcodeToAssembly(mockVMCommands))
                .Returns(expectedTranslation);

            File.WriteAllLines(inputFilename, mockVMCommands);

            outputStream.WriteLine("existing output");

            new VMFileTranslator(mockVMTranslator.Object)
                .TranslateFileToStream(inputFilename, outputStream);

            outputStream.Close();
            var actualOutput = File.ReadAllLines(outputFilename);

            Assert.Equal(expectedOutput, actualOutput);
        }

        public void Dispose()
        {
            outputStream.Dispose();
            File.Delete(inputFilename);
            File.Delete(outputFilename);
        }
    }
}