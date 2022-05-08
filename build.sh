#!/bin/sh

# build plugin dll
dotnet build --nologo

# download lf
if [ ! -e "${TMPDIR:-/tmp}/lf" ]; then
  cd "${TMPDIR:-/tmp}"
  wget "https://github.com/gokcehan/lf/releases/download/latest/lf-linux-amd64.tar.gz"
  tar xzf "lf-linux-amd64.tar.gz"
  cd "$OLDPWD"
fi

# add lf to scripts directory
cp "${TMPDIR:-/tmp}/lf" "./share/scripts/lf"

