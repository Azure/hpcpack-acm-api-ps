#!/bin/bash

name='HPC.ACM.API.PS'

function make
{
  dotnet publish -c Release
  if [ "$?" != "0" ]; then
    exit "$?"
  fi

  if [ -d "$name" ]; then
    rm -rf "$name"
  fi

  cp -r bin/Release/netstandard2.0/publish "$name"

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
