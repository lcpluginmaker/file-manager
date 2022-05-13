#!/bin/sh

# create cache dir
CACHE_DIR="${TMPDIR:-/tmp}/lc-fm"
PROJECT_FOLDER="$(pwd)"
mkdir -vp "$CACHE_DIR" || exit 1

# build plugin dll
dotnet build --nologo || exit 1

# clone lf
cd "$CACHE_DIR"
if [ ! -d "./lf" ]; then
  git clone "https://github.com/gokcehan/lf.git"
fi

# build lf
cd "./lf"
case "$APKG_BUILDER_OS" in
  lnx64) export GOOS="linux"; export GOARCH="amd64"; OUT="lf" ;;
  win64) export GOOS="windows"; export GOARCH="amd64"; OUT="lf.exe" ;;
  *) export GOOS="linux"; export GOARCH="amd64"; OUT="lf" ;;
esac
go build . || exit 1
cd "$PROJECT_FOLDER"

# copy lf binary
cp -v "$CACHE_DIR/lf/$OUT" "$PROJECT_FOLDER/share/scripts/$OUT"

