#include "Caff.h"
#include <sstream>

void Caff::ParseFromString(std::string caffString)
{
	uint64_t currentPosition = 1;
	while (currentPosition <= caffString.length())
	{
		uint64_t blockLength = *(uint64_t*)caffString.substr(currentPosition, 8).c_str() + 9;	// Leaves the id, takes the length, id and lenght isn't counted -> +9
		CaffBlock block;
		block.parseFromString(caffString.substr(currentPosition - 1, blockLength));	// Here we need the id as well -> -1
		blocks.insert(blocksIterator, block);
		currentPosition += blockLength;
	}
}
