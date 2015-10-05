#ifndef PARSER_H
#define PARSER_H
#include <string>
#include <array>
#include <locale>


class Parser
{
public:
    Parser();
    std::array<std::string,2> SplitCommand(std::string& command);
    std::string ToLower(std::string& x);
};

#endif // PARSER_H
