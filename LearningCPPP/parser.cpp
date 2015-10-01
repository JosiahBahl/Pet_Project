#include "parser.h"
#include <string>
#include <iostream>
#include <array>
Parser::Parser()
{

}

std::array<std::string,2> Parser::SplitCommand(std::string& command)
{
    std::array<std::string,2> commands;
    int pos = command.find(" ");
    if(pos != std::string::npos)
    {
        commands[0] = command.substr(0,pos);
        commands[1] = command.substr(pos+1,command.length());
    }
    else
    {
        commands[0] = command;
    }
    return commands;
}

