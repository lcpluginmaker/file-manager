#!/bin/sh

# create cache dir
CACHE_DIR="${TMPDIR:-/tmp}/lc-fm"
PROJECT_FOLDER="$(pwd)"
mkdir -vp "$CACHE_DIR" || exit 1

# build plugin dll
dotnet build --nologo || exit 1

# download lf
base_dl_url="https://github.com/gokcehan/lf/releases/latest/download"

get_lf_linux(){
  temp_file="$CACHE_DIR/lf.tar.gz"
  wget -nv -O "$temp_file" "$base_dl_url/lf-linux-amd64.tar.gz" || exit 1
  tar xzf "$temp_file" || exit 1
  cp -v "./lf" "$PROJECT_FOLDER/share/scripts/lf" || exit 1
}

get_lf_windows(){
  temp_file="$CACHE_DIR/lf.zip"
  wget -nv -O "$temp_file" "$base_dl_url/lf-windows-amd64.zip" || exit 1
  unzip "$temp_file" || exit 1
  cp -v "./lf.exe" "$PROJECT_FOLDER/share/scripts/lf.exe" || exit 1
}

case "$APKG_BUILDER_OS" in
  lnx64) get_lf_linux ;;
  win64) get_lf_windows ;;
  *) get_lf_linux ;;
esac

