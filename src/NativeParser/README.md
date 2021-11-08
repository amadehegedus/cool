# Native parser for CAFF files
This application parses the CAFF file into C++ classes and generates the bitmap images for all the animation frames, putting them next to the CAFF file.
## Building
In the native parser's folder, create a new folder, e.g. ```mkdir build```

After entering the new folder (```cd build```), create the cmake environment with ```cmake ..``` where the parameter point to the folder of the source files, in this case, it's the parent folder, so ..

Build the project with ```cmake --build .```

## Running
Enter the automatically created Debug folder

Run the app, with the parameter pointing to the CAFF file, like ```NativeParser ../../1.caff```

The output bitmaps will be placed next to the CAFF file.

The bitmaps for the first sample file can be found next to the source code.