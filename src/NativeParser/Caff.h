#pragma once
#include <string>
#include <cstdint>
#include <list>
#include "CaffBlock.h"

class Caff {
public:
	std::list<CaffBlock> blocks;
	std::list<CaffBlock>::iterator blocksIterator = blocks.begin();

	void ParseFromString(std::string caffString);

	void generateBitmapsForAllCiffs(std::string basePath, std::string originalFilename);
};