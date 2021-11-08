#pragma once

#include <string>
#include <cstdint>
#include <list>

class Ciff {
public:
	std::string magic;
	uint64_t headerSize;
	uint64_t contentSize;
	uint64_t width;
	uint64_t height;
	std::string caption;
	std::list<std::string> tags;
	std::list<std::string>::iterator tagsIterator = tags.begin();
	std::list<uint8_t> pixels;
	std::list<uint8_t>::iterator pixelsIterator = pixels.begin();

	void ParseFromString(std::string ciffString);

	void splitAndStoreTags(std::string s, std::string del = " ");

	void generateAndStoreBitMap(std::string fileName) const;
};
