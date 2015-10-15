#include "stdafx.h"
#include "parser.h"
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
        commands[0] = ToLower(commands[0]);
        commands[1] = ToLower(commands[1]);
    }
    else
    {
        commands[0] = ToLower(command);
    }
    return commands;
}

std::string Parser::ToLower(std::string& x)
{
    std::string temp = x;
    std::locale loc;
    for(int i = 0; i < temp.length(); i++)
    {
        temp[i] = std::tolower(temp[i], loc);
    }
    return temp;
}

