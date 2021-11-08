// NativeParser.cpp : Defines the entry point for the application.
//

#include "NativeParser.h"
#include "Ciff.h"
#include <fstream>
#include "Caff.h"

using namespace std;

int main(int argc, char* argv[])
{
	if (argc == 1)
	{
		cout << "Add the path of the CAFF file as a parameter! (relative to the exe file)" << endl;
		return -1;
	}
	std::string inputfilePath = std::string(argv[1]);

	std::ifstream is(inputfilePath, std::ifstream::binary);
	if (is)
	{
		is.seekg(0, is.end);
		size_t size = is.tellg();
		std::string buffer(size, ' ');
		is.seekg(0);
		is.read(&buffer[0], size);

		Caff caff;
		caff.ParseFromString(buffer);

		caff.generateBitmapsForAllCiffs(inputfilePath.substr(0, inputfilePath.rfind("/")), inputfilePath.substr(inputfilePath.rfind("/"), inputfilePath.rfind(".caff")));
	}
	else
	{
		cout << "File " + inputfilePath + " could not be found!" << endl;
	}

	return 0;
}
