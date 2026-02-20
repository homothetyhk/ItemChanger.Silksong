#!/usr/bin/env bash

set -e

# usage for humans: GITHUB_TOKEN=$(gh auth token) ./install-docfx-prerelease.sh
# usage for actions: in env, put GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
# see https://dotnet.github.io/docfx/#how-to-use-prerelease-version-of-docfx

version=$(gh api orgs/dotnet/packages/nuget/docfx/versions --jq '.[0].name')
url="https://nuget.pkg.github.com/dotnet/download/docfx/${version}/${version}.nupkg"
echo "Downloading docfx package from ${url}"

mkdir -p .github/packages
curl -XGET $url -o ".github/packages/docfx.${version}.nupkg" -L -H "Authorization: Bearer ${GITHUB_TOKEN}"
dotnet tool update docfx -g --prerelease --source .github/packages/
