#pragma once
#include <string>
#include <cstdint>
#include "Ciff.h"

class CaffBlockData {
public:
	// Header fields
	std::string magic;
	uint64_t headerSize;
	uint64_t numberOfAnimationBlocks;

	// Credit fields
	uint16_t creationYear;
	uint8_t creationMonth;
	uint8_t creationDay;
	uint8_t creationHour;
	uint8_t creationMinute;
	uint64_t creatorLength;
	std::string creator;

	// Animation fields
	uint64_t duration;
	Ciff ciff;

	void ParseFromString(std::string caffDataString, uint8_t type);
};