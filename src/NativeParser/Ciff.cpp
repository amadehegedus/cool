#include <string>
#include <cstdint>
#include <list>
#include "Ciff.h"

void Ciff::ParseFromString(std::string ciffString)
{
	magic = ciffString.substr(0, 4);
	headerSize = *(uint64_t*)ciffString.substr(4, 8).c_str();
	contentSize = *(uint64_t*)ciffString.substr(12, 8).c_str();
	width = *(uint64_t*)ciffString.substr(20, 8).c_str();
	height = *(uint64_t*)ciffString.substr(28, 8).c_str();

	//Caption
	uint64_t endLinePosition = ciffString.find('\n', 36);
	uint64_t captionLength = endLinePosition - 36;
	caption = ciffString.substr(36, captionLength);

	//Tags
	int tagsLength = headerSize - 36 - captionLength - 1;	//header - other fields -\n
	std::string tagsString = ciffString.substr(36 + captionLength + 1, tagsLength);

	uint64_t start = 0;
	uint64_t end = tagsString.find('\0');
	while (end != -1) {
		tags.insert(tagsIterator, tagsString.substr(start, end - start));
		start = end + 1;
		end = tagsString.find('\0', start);
	}

	//Pixels
	std::string pixelString = ciffString.substr(headerSize);
	for (int i = 0; i < contentSize; i++)
	{
		pixels.insert(pixelsIterator, pixelString[i]);
	}
}

void Ciff::splitAndStoreTags(std::string s, std::string del)
{
	uint64_t start = 0;
	uint64_t end = s.find(del);
	while (end != -1) {
		tags.insert(tagsIterator, s.substr(start, end - start));
		start = end + del.size();
		end = s.find(del, start);
	}
	tags.insert(tagsIterator, s.substr(start, end - start));
}

