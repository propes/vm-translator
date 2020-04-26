#!/bin/bash
coverlet VMTranslator.Lib.Tests/bin/Debug/netcoreapp2.2/VMTranslator.Lib.Tests.dll \
    --target "dotnet" \
    --targetargs "test --no-build" \
    --output "./lcov.info" \
    --format lcov

genhtml lcov.info --output-directory coverage-report -q

chromium-browser coverage-report/index.html