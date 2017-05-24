// Copyright 2016 Interaxon, Inc.
#pragma once

#include <memory>
#include "api/bridge_muse_manager.h"

namespace interaxon { namespace bridge {

/**
 * Extends MuseManager to provide Windows specific functionality.
 *
 * MuseManager provides access to all Muse headbanads connected to this Windows
 * device.
 */
class MuseManagerWindows: public MuseManager {

public:

    /**          
     * Returns the shared instance of the MuseManager for Windows.
     *
     * \return the shared instance of the MuseManager for Windows.
     * \deprecated Use MuseManagerWindows::get_instance()
     */
    static std::shared_ptr<MuseManagerWindows> getInstance();

    /**
     * Returns the shared instance of the MuseManager for Windows.
     *
     * \return the shared instance of the MuseManager for Windows.
     */
    static std::shared_ptr<MuseManagerWindows> get_instance();
};

}
}
