# CMakeList.txt : CMake project for NativeParser, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

project ("NativeParser")

# Add source to this project's executable.
add_executable (NativeParser "NativeParser.cpp" "NativeParser.h" "CaffBlock.cpp"   "Ciff.cpp"    "Ciff.h" "CaffBlock.h"    "CaffBlockData.h" "CaffBlockData.cpp" "Caff.cpp" "Caff.h")

# TODO: Add tests and install targets if needed.
