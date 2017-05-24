// AUTOGENERATED FILE - DO NOT MODIFY!
// This file generated by Djinni from muse_file.djinni

#pragma once

#include <cstdint>
#include <string>
#include <utility>
#include <vector>

namespace interaxon { namespace bridge {

/**
 * Data struct corresponds to a DSP message in the .muse file format.
 * This message is very generic and allows you to save custom messages.
 * For more information on the file format see: http://developer.choosemuse.com/file-formats/muse
 */
struct DspData final {
    /** Type of your message */
    std::string type;
    /** array of floating point data */
    std::vector<double> float_array;
    /** array of integer data */
    std::vector<int64_t> int_array;
    /** version of your message format */
    std::string version;

    DspData(std::string type_,
            std::vector<double> float_array_,
            std::vector<int64_t> int_array_,
            std::string version_)
    : type(std::move(type_))
    , float_array(std::move(float_array_))
    , int_array(std::move(int_array_))
    , version(std::move(version_))
    {}
};

} }  // namespace interaxon::bridge