"use strict";

exports.__esModule = true;
exports.default = dateFnsLocalizer;
exports.defaultFormats = void 0;

var _formatWithOptions = _interopRequireDefault(require("date-fns/fp/formatWithOptions"));

var _parseWithOptions = _interopRequireDefault(require("date-fns/fp/parseWithOptions"));

var _addYears = _interopRequireDefault(require("date-fns/fp/addYears"));

var _enUS = _interopRequireDefault(require("date-fns/locale/en-US"));

var _configure = _interopRequireDefault(require("react-widgets/lib/configure"));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var endOfDecade = (0, _addYears.default)(10);
var endOfCentury = (0, _addYears.default)(100);

function getYear(date, culture, localizer) {
  return localizer.format(date, 'yyyy', culture);
}

function decade(date, culture, localizer) {
  return getYear(date, culture, localizer) + ' - ' + getYear(endOfDecade(date), culture, localizer);
}

function century(date, culture, localizer) {
  return getYear(date, culture, localizer) + ' - ' + getYear(endOfCentury(date), culture, localizer);
}

var defaultFormats = {
  date: 'P',
  time: 'pp',
  default: 'Pp',
  header: 'MMMM yyyy',
  footer: 'PPPP',
  weekday: 'cccccc',
  dayOfMonth: 'dd',
  month: 'MMM',
  year: 'yyyy',
  decade: decade,
  century: century
};
/**
 * Configures localization of [react-widgets](http://jquense.github.io/react-widgets/) by calling
 * `configure.setDateLocalizer`
 * @static
 * @param {Object} formats Confguration options.
 * @param {string|Object|function} formats.default  the default date display format, generally a "long" format showing
 *   both date and time
 * @param {string|Object|function} formats.date A date only format
 * @param {string|Object|function} formats.time A time only format
 * @param {string|Object|function} formats.header The heading of the Calendar month view, contextualizes the current
 *   month, e.g. "Jan 2014"
 * @param {string|Object|function} formats.footer The Calendar footer format, for displaying Today's date
 * @param {string|Object|function} formats.dayOfMonth The day of the month
 * @param {string|Object|function} formats.month  Month name, used in the Year view of the Calendar
 * @param {string|Object|function} formats.year year format, used in the Decade view of the Calendar
 * @param {string|Object|function} formats.decade a decade format, used in the Century view of the Calendar,
 *  eg. "2010 - 2019"
 * @param {string|Object|function} formats.century  A century format, used the in the Calendar heading
 * @param {Object} locales Supported date-fns locales to include in the bundle
 * @example
 * import dateFnsLocalizer, { defaultFormats } from 'react-widgets-dates'
 * dateFnsLocalizer()
 * // => Works out of the box with default formats (defaultFormats) and the `en-US` locale
 * const newFormats = Object.assign(defaultFormats, { default: 'mmm YY' })
 * dateFnsLocalizer({ formats: newFormats })
 * // => Uses new configuration
 *
 * import locales from 'date-fns/locale'
 * dateFnsLocalizer({ formats: newFormats, locales })
 * // => Includes all available locales
 *
 * import enGB from 'date-fns/locale/en-GB'
 * import de from 'date-fns/locale/de'
 * dateFnsLocalizer({ locales: { 'en-GB': enGB, 'de': de } })
 * // => Include only the locales you need to limit the bundled size
 */

exports.defaultFormats = defaultFormats;

function dateFnsLocalizer(_temp) {
  var _ref = _temp === void 0 ? {} : _temp,
      _ref$formats = _ref.formats,
      formats = _ref$formats === void 0 ? defaultFormats : _ref$formats,
      _ref$locales = _ref.locales,
      locales = _ref$locales === void 0 ? {} : _ref$locales;

  function getLocale(culture) {
    return locales[culture] || _enUS.default;
  }

  function format(value, format, culture) {
    return (0, _formatWithOptions.default)({
      locale: getLocale(culture)
    }, format, value);
  }

  function parse(value, format, culture) {
    var result = (0, _parseWithOptions.default)({
      locale: getLocale(culture)
    }, new Date(), format, value);

    if (result.toString() === 'Invalid Date') {
      return null;
    }

    return result;
  }

  function firstOfWeek(culture) {
    var _getLocale = getLocale(culture),
        options = _getLocale.options;

    return options && options.weekStartsOn || 0;
  }

  _configure.default.setDateLocalizer({
    formats: formats,
    firstOfWeek: firstOfWeek,
    parse: parse,
    format: format
  });
}
