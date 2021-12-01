#include <string>
#include <cstdint>
#include <list>
#include "Ciff.h"
#include <fstream>

void Ciff::ParseFromString(std::string ciffString)
{
	magic = ciffString.substr(0, 4);
	if (magic != "CIFF")
		throw "Wrong Ciff format, wrong magic!";
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

void Ciff::generateAndStoreBitMap(std::string fileName) const
{
	// https://dev.to/muiz6/c-how-to-write-a-bitmap-image-from-scratch-1k6m
	struct BmpHeader {
		char bitmapSignatureBytes[2] = { 'B', 'M' };
		uint32_t sizeOfBitmapFile;
		uint32_t reservedBytes = 0;
		uint32_t pixelDataOffset = 54;
	} bmpHeader;
	bmpHeader.sizeOfBitmapFile = 54 + width * height * 3;

	struct BmpInfoHeader {
		uint32_t sizeOfThisHeader = 40;
		int32_t width;
		int32_t height;
		uint16_t numberOfColorPlanes = 1; // must be 1
		uint16_t colorDepth = 24;
		uint32_t compressionMethod = 0;
		uint32_t rawBitmapDataSize = 0;
		int32_t horizontalResolution = 3780; // in pixel per meter
		int32_t verticalResolution = 3780; // in pixel per meter
		uint32_t colorTableEntries = 0;
		uint32_t importantColors = 0;
	} bmpInfoHeader;
	bmpInfoHeader.width = (int32_t)width;
	bmpInfoHeader.height = (int32_t)height * -1;	// negative: start at the top left, if it's positive it starts at the bottom left

	std::ofstream fout(fileName + ".bmp", std::ios::binary);

	fout.write(bmpHeader.bitmapSignatureBytes, 2);
	fout.write((char*)&bmpHeader.sizeOfBitmapFile, sizeof(uint32_t));
	fout.write((char*)&bmpHeader.reservedBytes, sizeof(uint32_t));
	fout.write((char*)&bmpHeader.pixelDataOffset, sizeof(uint32_t));

	fout.write((char*)&bmpInfoHeader.sizeOfThisHeader, sizeof(uint32_t));
	fout.write((char*)&bmpInfoHeader.width, sizeof(int32_t));
	fout.write((char*)&bmpInfoHeader.height, sizeof(int32_t));
	fout.write((char*)&bmpInfoHeader.numberOfColorPlanes, sizeof(uint16_t));
	fout.write((char*)&bmpInfoHeader.colorDepth, sizeof(uint16_t));
	fout.write((char*)&bmpInfoHeader.compressionMethod, sizeof(uint32_t));
	fout.write((char*)&bmpInfoHeader.rawBitmapDataSize, sizeof(uint32_t));
	fout.write((char*)&bmpInfoHeader.horizontalResolution, sizeof(int32_t));
	fout.write((char*)&bmpInfoHeader.verticalResolution, sizeof(int32_t));
	fout.write((char*)&bmpInfoHeader.colorTableEntries, sizeof(uint32_t));
	fout.write((char*)&bmpInfoHeader.importantColors, sizeof(uint32_t));		

	for (const auto& pixel : pixels)
	{
		fout.write((char*)&pixel, 1);
	}

	fout.close();
}


