#ifndef PARSER_H
#define PARSER_H
#include <string>
#include <array>


class Parser
{
public:
    Parser();
    std::array<std::string,2> SplitCommand(std::string& command);
};

#endif // PARSER_H
