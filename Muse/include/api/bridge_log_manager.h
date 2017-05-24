// AUTOGENERATED FILE - DO NOT MODIFY!
// This file generated by Djinni from logging.djinni

#pragma once

#include <cstdint>
#include <memory>
#include <string>

namespace interaxon { namespace bridge {

class LogListener;
enum class Severity;

/**
 * Manages the listener for any logging events generated by LibMuse.
 *
 * To disable logging:
 *
 * \if (ANDROID_ONLY || IOS_ONLY)
 *     setLogListener(null);
 * \elseif (WINDOWS_ONLY)
 *	  \code{.cpp} set_log_listener(null);\endcode
 * \else
 * MISSING_DOCUMENTATION
 * \endif
 *
 * To install a custom log listener for your application:
 *
 * \if (ANDROID_ONLY || IOS_ONLY)
 *     setLogListener(your_log_listener);
 * \elseif (WINDOWS_ONLY)
 *	  \code{.cpp} set_log_listener(your_log_listener);\endcode
 * \else
 * MISSING_DOCUMENTATION
 * \endif
 *
 * To restore default logging to standard error (if you have previously
 * changed the log listener):
 *
 * \if (ANDROID_ONLY || IOS_ONLY)
 *     setLogListener(makeDefaultLogListener());
 * \elseif (WINDOWS_ONLY)
 *	  \code{.cpp} set_log_listener(make_default_log_listener());\endcode
 * \else
 * MISSING_DOCUMENTATION
 * \endif
 *
 * An instance of the default listener is installed unless another
 * listener is supplied. The default log listener prints all messages to standard error
 * as they show up, without any buffering, using the following formatting rules:
 * \li Raw logs are printed directly without any formatting or newline.
 * \li Other messages are formatted to pretty-print with the tag, timestamp,
 * severity, and log message on a single line (prepending a newline if the
 * last message printed was raw.)
 *
 * For example, suppose that these packets showed up in order: {info, false, "A", 4,
 * "thing A"}, {info, true, "FOO", 1, "+"}, {err, true, "BAR", 3, "-"},
 * {warn, false, "B", 2, "thing B"} where the second parameter idicates if
 * this is a raw log message.  The log output would be formatted
 * roughly as such:
 *
 *     A [I 4.000] thing A
 *     +-
 *     B [W 2.000] thing B
 *
 * In order to provide this formatting without buffering or losing messages,
 * the default log listener uses a mutex, effectively making the logging
 * subsystem single-threaded and IO-bound.
 * \sa \classlink{LogListener}
 */
class LogManager {
public:
    virtual ~LogManager() {}

    /**
     * Returns the shared log manager instance.
     * \return The shared log manager instance.
     */
    static std::shared_ptr<LogManager> instance();

    /**
     * Constructs and returns a default log listener.
     *
     * \return an instance of the default log listener.
     */
    virtual std::shared_ptr<LogListener> make_default_log_listener() = 0;

    /**
     * Sets the log listener.
     *
     * There is only one log listener active at a time; hence this is just a
     * plain setter rather than a register / unregister pair.
     *
     * \param listener the LogListener to use.
     */
    virtual void set_log_listener(const std::shared_ptr<LogListener> & listener) = 0;

    /**
     * Set the minimum log severity.
     *
     * Messages at or above this severity will be passed to the configured log
     * listener. Messages below this severity will be dropped.
     * \param severity the minimum log severity to log.
     */
    virtual void set_minimum_severity(Severity severity) = 0;

    /**
     * Write a log line.
     *
     * This method sends messages to the configured log listener. It is exposed
     * here primarily so that LibMuse can access it from its own
     * platform-specific code, but there's nothing stopping you from using it
     * and doing all your logging through LibMuse. Except please don't, because
     * we may change the API at any time, and why would you?
     *
     * The log's timestamp is the number of seconds since this log manager was
     * created.
     * \param severity The severity of this log message.
     * \param raw \c true if this is a raw log message (no formatting), \c false otherwise
     * \param tag The tag for this message.
     * \param message The log message.
     */
    virtual void write_log(Severity severity, bool raw, const std::string & tag, const std::string & message) = 0;

    /**
     * Return the current timestamp.
     *
     * The returned value is in microseconds since some epoch -- January 1 1970
     * on UNIX, but possibly other values on other systems.
     *
     * \return The current timestamp.
     */
    virtual int64_t get_timestamp() = 0;

    /**
     * Return the time elapsed in seconds since the passed timestamp.
     *
     * \param timestamp The prior timestamp.
     */
    virtual double time_since(int64_t timestamp) = 0;
};

} }  // namespace interaxon::bridge
