#pragma once

#include <string>
#include "CaffBlockData.h"

class CaffBlock {
public:
	uint8_t id;
	uint64_t length;
	CaffBlockData data;

	void parseFromString(std:: string caffBlockString);
};
