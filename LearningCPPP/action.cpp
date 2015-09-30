#include "action.h"
#include <string>

Action::Action(std::string key, std::string (*function)(std::string))
{
    _keyword = key;
    _function = function;
}

