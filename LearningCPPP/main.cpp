#include <iostream>
#include <parser.h>
#include <thread>
#include <map>

using namespace std;

typedef std::string (*Action)();

std::map<std::string, Action> _functions;

std::string Look()
{
    return "I have looked";
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
                    std::map<std::string, std::string(*)()>::iterator search= _functions.find(commands[0]);
                    if(search != _functions.end())
                    {
                        Action x = search->second;
                       std::cout<<(*x)()<<std::endl;
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
}

int main()
{
    CreateWorld();
    std::thread t (ProcessComands);
    t.join();
    return 0;
}

