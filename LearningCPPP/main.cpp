#include <iostream>
#include <parser.h>
#include <worldmap.h>
#include <thread>
#include <map>

using namespace std;
//
typedef std::string (*Action)(std::string x);
//
std::map<std::string, Action> _functions;
//
WorldMap _map = WorldMap();
//
std::string Look(std::string target)
{
    return _map.GetCurrentRoom().getLongDesc();
}

std::string GoTo(std::string direction)
{
    std::string returnString = "The room does not have an exit that way";
    if(direction == "north")
    {
        if(_map.GetCurrentRoom().hasExit('n'))
        {
            _map.GoToRoom(_map._row+1, _map._colum);
            returnString = _map.GetCurrentRoom().getDesc();
        }
        else
        {
        }
    }
    else if (direction == "east")
    {
        if(_map.GetCurrentRoom().hasExit('e'))
        {
            _map.GoToRoom(_map._row, _map._colum+1);
            returnString = _map.GetCurrentRoom().getDesc();
        }
        else
        {
        }
    }
    else if (direction == "south")
    {
        if(_map.GetCurrentRoom().hasExit('s'))
        {
            _map.GoToRoom(_map._row-1, _map._colum);
            returnString = _map.GetCurrentRoom().getDesc();
        }
        else
        {
        }
    }
    else if (direction == "west")
    {
        if(_map.GetCurrentRoom().hasExit('w'))
        {
            _map.GoToRoom(_map._row, _map._colum-1);
            returnString = _map.GetCurrentRoom().getDesc();
        }
        else
        {
        }
    }
    else
    {
        returnString = direction+" is not a proper direction";
    }
    return returnString;
}

void ProcessComands()
{
    bool end = false;

    std::string command;

    std::array<std::string, 2> commands = {""};

    Parser parser;

    while(!end)
    {
       std::cout<<"Enter command"<<std::endl;
       std::getline(std::cin,command);
       if(command == "exit")
       {
           end = true;
       }
       else
       {
           commands[0] = "";
           commands[1] = "";
           //
           commands = parser.SplitCommand(command);
           if(commands[0] != "")
           {
               if(commands[1] != "")
               {
                    std::map<std::string, std::string(*)(std::string x)>::iterator search = _functions.find(commands[0]);
                    if(search != _functions.end())
                    {
                        Action x = search->second;
                       std::cout<<(*x)(commands[1])<<std::endl;
                    }
                    else
                    {

                    }
               }
               else
               {
                    std::cout<<commands[0]+" to what?"<<std::endl;
               }
            }
            else
            {
               std::cout<<"You did not enter a command."<<std::endl;
            }
       }
    }
}

void CreateWorld()
{
    _functions["look"] = Look;
    _functions["go"] = GoTo;
    _map.CreateMap();
}

int main()
{
    CreateWorld();
    std::thread t (ProcessComands);
    t.join();
    return 0;
}

