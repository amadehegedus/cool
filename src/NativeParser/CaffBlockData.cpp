#include "CaffBlockData.h"

void CaffBlockData::ParseFromString(std::string caffDataString, uint8_t type)
{
	switch (type)
	{
	case 1: 
	{
		magic = caffDataString.substr(0, 4);
		headerSize = *(uint64_t*)caffDataString.substr(4, 8).c_str();
		numberOfAnimationBlocks = *(uint64_t*)caffDataString.substr(12, 8).c_str();
		break;
	}
	case 2:
	{
		creationYear = *(uint16_t*)caffDataString.substr(0, 2).c_str();
		creationMonth = caffDataString[2];
		creationDay = caffDataString[3];
		creationHour = caffDataString[4];
		creationMinute = caffDataString[5];
		creatorLength = *(uint64_t*)caffDataString.substr(6, 8).c_str();
		creator = caffDataString.substr(14);
		break;
	}
	case 3:
	{
		duration = *(uint64_t*)caffDataString.substr(0, 8).c_str();
		ciff.ParseFromString(caffDataString.substr(8));
		break;
	}
	default: break;
	}
}
