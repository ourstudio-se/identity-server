#!/usr/bin/env bash
set -euo pipefail

rm -rf nuget
mkdir nuget

dotnet tool restore

CMD="dotnet list package --vulnerable --include-transitive"
EXPECT="following vulnerable packages"

pushd ./src/Storage/src > /dev/null
dotnet restore --verbosity quiet
OUTPUT=$($CMD)
if test $(echo "$OUTPUT" | grep -cm 1 "${EXPECT}") -ne 0; then
    $CMD
    exit 1
fi
popd > /dev/null

pushd ./src/IdentityServer/src > /dev/null
dotnet restore --verbosity quiet
OUTPUT=$($CMD)
if test $(echo "$OUTPUT" | grep -cm 1 "${EXPECT}") -ne 0; then
    $CMD
    exit 1
fi
popd > /dev/null

pushd ./src/EntityFramework.Storage/src > /dev/null
dotnet restore --verbosity quiet
OUTPUT=$($CMD)
if test $(echo "$OUTPUT" | grep -cm 1 "${EXPECT}") -ne 0; then
    $CMD
    exit 1
fi
popd > /dev/null

pushd ./src/EntityFramework/src > /dev/null
dotnet restore --verbosity quiet
OUTPUT=$($CMD)
if test $(echo "$OUTPUT" | grep -cm 1 "${EXPECT}") -ne 0; then
    $CMD
    exit 1
fi
popd > /dev/null

pushd ./src/AspNetIdentity/src > /dev/null
dotnet restore --verbosity quiet
OUTPUT=$($CMD)
if test $(echo "$OUTPUT" | grep -cm 1 "${EXPECT}") -ne 0; then
    $CMD
    exit 1
fi
popd > /dev/null

pushd ./src/RedisStore/src > /dev/null
dotnet restore --verbosity quiet
OUTPUT=$($CMD)
if test $(echo "$OUTPUT" | grep -cm 1 "${EXPECT}") -ne 0; then
    $CMD
    exit 1
fi
popd > /dev/null
