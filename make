#!/bin/bash

name='HPC.ACM.API.PS'
config=${Config:-Release}

function make
{
  dotnet publish -c "$config"
  if [ "$?" != "0" ]; then
    exit "$?"
  fi

  if [ -d "$name" ]; then
    rm -rf "$name"
  fi

  cp -r "bin/$config/netstandard2.0/publish" "$name"

  cp "${name}.psd1" "${name}/"
}

function clean
{
  if [ -d "$name" ]; then
    rm -rf "$name"
  fi
}

if [ "$1" = "-c" ]; then
  clean
else
  make
fi
