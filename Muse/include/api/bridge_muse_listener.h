// AUTOGENERATED FILE - DO NOT MODIFY!
// This file generated by Djinni from sdk_bridge.djinni

#pragma once

namespace interaxon { namespace bridge {

/**
 * Implement this interface to receive callbacks whenever a Muse headband is
 * discovered after calling
 * \inlinelink{MuseManager.startListening(),IXNMuseManager::startListening:,MuseManager::start_listening()}
 */
class MuseListener {
public:
    virtual ~MuseListener() {}

    /**
     * Called when the list of Muses detected by MuseManager has changed.
     * <ul>
     * <li>You will receive 1 callback for each headband discovered and added to the list.
     * \if ANDROID_ONLY
     * For \muse2014
     * headbands, there will be a callback for each headband paired with the device even
     * if the headband is powered off.
     * \elseif IOS_ONLY
     * For \muse2014
     * headbands, there will be a callback for each headband paired with the device only
     * when the headband is powered on and connected to the device.
     * \elseif WINDOWS_ONLY
     * For \muse2014 and \muse2016
     * headbands, there will be a callback for each headband paired with the system even
     * if the headband is powered off.
     * \else
     * MISSING_DOCUMENTATION
     * \endif
     * <li>You will receive 1 callback for each removal operation.
     * If more than 1 headband is removed as the result of a single removal operation,
     * you will only receive 1 callback instead of 1 callback for each headband.
     * </ul>
     */
    virtual void muse_list_changed() = 0;
};

} }  // namespace interaxon::bridge
