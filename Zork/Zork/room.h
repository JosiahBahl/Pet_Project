#ifndef ROOM_H
#define ROOM_H
#include "stdafx.h"
#include <string>
#include <array>
#include <iostream>
#include "item.h"

class Room
{
protected:
    std::string _roomName;
    std::string _roomDesc;
    std::string _longDesc;
    //North,East,South,West
    std::array<char, 4> _exits;
	ItemBase * _item;
public:
    Room();
    Room(std::string& name, std::string& desc, std::string& lDesc, std::array<char, 4> exits);
    Room(std::string& name, std::string& desc, std::string& lDesc, std::array<char, 4> exits, ItemBase &item);
    Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits);
    Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits, ItemBase &item);
    std::string getName();
    std::string getDesc();
    std::string getLongDesc();
    std::array<char, 4> getExits();
    ItemBase* getItem();
    void setName(std::string& x);
    void setDesc(std::string& x);
    void setLongDesc(std::string& x);
    void setExits(std::array<char, 4> x);
    bool hasExit(char x);
    bool hasItem();
    std::string PrintExits();
};

#endif // ROOM_H
