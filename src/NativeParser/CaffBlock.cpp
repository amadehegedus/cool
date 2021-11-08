#include <string>
#include <cstdint>
#include "CaffBlock.h"

void CaffBlock::parseFromString(std:: string caffBlockString)
{
	id = caffBlockString[0];
	length = *(uint64_t*)caffBlockString.substr(1, 8).c_str();
	data.ParseFromString(caffBlockString.substr(9), id);
}
