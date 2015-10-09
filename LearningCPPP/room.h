#ifndef ROOM_H
#define ROOM_H
#include <string>
#include <array>
#include <iostream>

class Room
{
private:
    std::string _roomName;
    std::string _roomDesc;
    std::string _longDesc;
    //North,East,South,West
    std::array<char, 4> _exits;
public:
    Room();
    Room(std::string& name, std::string& desc, std::string& lDesc, std::array<char, 4> exits);
    Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits);
    std::string getName();
    std::string getDesc();
    std::string getLongDesc();
    std::array<char, 4> getExits();
    void setName(std::string& x);
    void setDesc(std::string& x);
    void setLongDesc(std::string& x);
    void setExits(std::array<char, 4> x);
    bool hasExit(char x);
};

#endif // ROOM_H
