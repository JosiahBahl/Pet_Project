#ifndef ACTION_H
#define ACTION_H
#include <string>

class Action
{
private:
    std::string _keyword;
    std::string (*_function)(std::string);
public:
    Action(std::string key, std::string (*function)(std::string));
};

#endif // ACTION_H
