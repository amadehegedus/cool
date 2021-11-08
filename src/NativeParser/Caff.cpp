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

void Caff::generateBitmapsForAllCiffs(std::string basePath, std::string originalFilename)
{
	uint64_t currentBlock = 1;
	for (const auto& block : blocks)
	{
		if (block.id == 3)	// Animation frame
		{
			std::string filename = originalFilename + "-bitmap" + std::to_string(currentBlock++);
			std::string fullPath = basePath.empty() ? filename : basePath + "/" + filename;

			block.data.ciff.generateAndStoreBitMap(fullPath);
		}
	}
}
