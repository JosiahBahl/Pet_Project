#include <iostream>
#include <parser.h>
#include <thread>
#include <array>
#include <action.h>

using namespace std;

std::array<Action::Action, 5> _actions;
void ProcessComands()
{
    bool end = false;

    std::string command;

    std::array<std::string, 2> commands;

    Parser parser;

    while(!end)
    {
       std::cout<<"Enter command"<<std::endl;
       std::getline(std::cin,command);
       if(command == "Exit")
       {
           end = true;
       }
       else
       {
           commands = parser.SplitCommand(command);
           std::cout<<commands[0]+" | "+commands[1]<<std::endl;
       }
    }
}

int main()
{
    std::thread t (ProcessComands);
    t.join();
    return 0;
}

