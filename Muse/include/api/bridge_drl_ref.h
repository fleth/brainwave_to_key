// AUTOGENERATED FILE - DO NOT MODIFY!
// This file generated by Djinni from packets.djinni

#pragma once

#include <functional>

namespace interaxon { namespace bridge {

/**
 * \if IOS_ONLY
 * \file
 * \endif
 * Represents the data mapping in a DRL_REF packet.
 * \sa \enumlink{MuseDataPacketType,DRL_REF,IXNMuseDataPacketTypeDrlRef}
 * \sa \functionlink{MuseDataPacket,getDrlRefValue,get_drl_ref_value}
 */
enum class DrlRef : int {
    /** DRL sensor */
    DRL,
    /** REF sensor */
    REF,
};

} }  // namespace interaxon::bridge

namespace std {

template <>
struct hash<::interaxon::bridge::DrlRef> {
    size_t operator()(::interaxon::bridge::DrlRef type) const {
        return std::hash<int>()(static_cast<int>(type));
    }
};

}  // namespace std
