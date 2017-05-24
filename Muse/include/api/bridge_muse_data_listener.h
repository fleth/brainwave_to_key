// AUTOGENERATED FILE - DO NOT MODIFY!
// This file generated by Djinni from listeners.djinni

#pragma once

#include <memory>

namespace interaxon { namespace bridge {

class Muse;
class MuseDataPacket;
struct MuseArtifactPacket;

/**
 * Implement this interface to receive data from the headband.  The
 * data may be either data packets or artifact packets.
 * Check out \classlink{MuseDataPacketType} for information about all
 * acceptable data packet types. All packets
 * can be handled in one listener or you can specify a separate listener for every
 * specific packet type.
 *
 * A packet is only sent if a listener was registered to listen for this packet type.
 */
class MuseDataListener {
public:
    virtual ~MuseDataListener() {}

    /**
     * Handler method for Muse data packets
     *
     * \warning It is important that you do not perform any computation
     * intensive tasks in this callback. This would result in significant
     * delays in all the listener callbacks from being called. You should
     * delegate any intensive tasks to another thread or schedule it to run
     * with a delay through handler/scheduler for the platform.
     *
     * However, you can register/unregister listeners in this callback.
     * All previously registered listeners would still receive callbacks
     * for this current event. On subsequent events, the newly registered
     * listeners will be called. For example, if you had 2 listeners 'A' and 'B'
     * for this event. If, on the callback for listener A, listener A unregisters
     * all listeners and registers a new listener 'C' and then in the callback for
     * listener 'B', you unregister all listeners again and register a new listener
     * 'D'. Then on the subsequent event callback, only listener D's callback
     * will be invoked.
     *
     * \param packet The data packet
     * \param muse   The \classlink{Muse} that sent the data packet.
     */
    virtual void receive_muse_data_packet(const std::shared_ptr<MuseDataPacket> & packet, const std::shared_ptr<Muse> & muse) = 0;

    /**
     * Handler method for Muse artifact packets. Packet is sent only if
     * listener was registered to listen for such packets.
     *
     * \warning It is important that you do not perform any computation
     * intensive tasks in this callback. This would result in significant
     * delays in all the listener callbacks from being called. You should
     * delegate any intensive tasks to another thread or schedule it to run
     * with a delay through handler/scheduler for the platform.
     *
     * However, you can register/unregister listeners in this callback.
     * All previously registered listeners would still receive callbacks
     * for this current event. On subsequent events, the newly registered
     * listeners will be called. For example, if you had 2 listeners 'A' and 'B'
     * for this event. If, on the callback for listener A, listener A unregisters
     * all listeners and registers a new listener 'C' and then in the callback for
     * listener 'B', you unregister all listeners again and register a new listener
     * 'D'. Then on the subsequent event callback, only listener D's callback
     * will be invoked.
     *
     * \param packet The artifact packet
     * \param muse   The \classlink{Muse} that sent the artifact packet.
     */
    virtual void receive_muse_artifact_packet(const MuseArtifactPacket & packet, const std::shared_ptr<Muse> & muse) = 0;
};

} }  // namespace interaxon::bridge
