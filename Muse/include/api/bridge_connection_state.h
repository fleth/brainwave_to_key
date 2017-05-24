// AUTOGENERATED FILE - DO NOT MODIFY!
// This file generated by Djinni from sdk_bridge.djinni

#pragma once

#include <functional>

namespace interaxon { namespace bridge {

/**
 * \if IOS_ONLY
 * \file
 * \endif
 * Lists all possible connection states
 */
enum class ConnectionState : int {
    /** Initial state */
    UNKNOWN,
    /** This state is set if the connection was correctly established. */
    CONNECTED,
    /** This state is set while trying to establish a connection. */
    CONNECTING,
    /**
     * This state is set in case of an unsuccessful connection operation
     * or after execution of disconnect method.
     */
    DISCONNECTED,
    /**
     * This state is set when the connection succeeded but the headband's
     * firmware is out of date -- if this occurs, you should instruct your users
     * to use the %Muse app to upgrade their firmware.
     */
    NEEDS_UPDATE,
};

} }  // namespace interaxon::bridge

namespace std {

template <>
struct hash<::interaxon::bridge::ConnectionState> {
    size_t operator()(::interaxon::bridge::ConnectionState type) const {
        return std::hash<int>()(static_cast<int>(type));
    }
};

}  // namespace std