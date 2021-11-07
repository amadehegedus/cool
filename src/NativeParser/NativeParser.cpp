// NativeParser.cpp : Defines the entry point for the application.
//

#include "NativeParser.h"
#include "Ciff.h"
#include <fstream>
#include "Caff.h"

using namespace std;

int main()
{
	std::ifstream is("../../../3.caff", std::ifstream::binary);
	if (is)
	{
		is.seekg(0, is.end);
		size_t size = is.tellg();
		std::string buffer(size, ' ');
		is.seekg(0);
		is.read(&buffer[0], size);

		Caff caff;
		caff.ParseFromString(buffer);
	}
	else
	{
		cout << "File could not be found!" << endl;
	}

	return 0;
}
