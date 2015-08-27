using System;
using System.Collections.Generic;
using System.Linq;
using Editor.ViewModels;
using TraktorMapping.TSI.Format;

namespace Editor.Controllers
{
    public class MainWindowController
    {
        public static List<MappingModel> CreateModel(Device device)
        {
            List<MappingModel> models = new List<MappingModel>();

            foreach (Mapping mapping in device.Data.Mappings.List.Mappings)
            {
                string midinote = device.Data.Mappings.MidiBindings.Bindings.First(b => b.BindingId == mapping.MidiNoteBindingId).MidiNote;

                models.Add(new MappingModel(mapping.MidiNoteBindingId,
                    midinote,
                    mapping.TraktorControl.Name,
                    mapping.TraktorControl.Target,
                    mapping.Settings.Comment,
                    mapping.Settings.Deck,
                    mapping.Type,
                    mapping.Settings.ControllerType,
                    mapping.Settings.InteractionMode,
                    mapping.Settings.AutoRepeat,
                    mapping.Settings.Invert,
                    mapping.Settings.SoftTakeover,
                    mapping.Settings.RotarySensitivity,
                    mapping.Settings.RotaryAcceleration,
                    mapping.Settings.LedInvert,
                    mapping.Settings.Resolution));
            }

            return models;
        }

        public static SaveModelResult SaveModel(IList<MappingModel> model, Device device)
        {
            SaveModelResult result = SaveModelResult.Success;
            foreach (MappingModel map in model.Where(m => m.HasChanges))
            {
                if (map.TraktorCommand == "Unknown")
                {
                    // We don't know this command so we won't save anything against it. At some point we need to complete the list of known traktor commands
                    result = SaveModelResult.SuccessWithNonSavedUnknownMappings;
                    continue;
                }


                // TODO: This will not work for new mappings as single will throw an exception
                var mappings = device.Data.Mappings.List.Mappings;
                Mapping deviceMap = mappings.Single(m => m.MidiNoteBindingId == map.Id && m.Type == map.Type);
                deviceMap.TraktorControlId = TraktorControl.AllIn.First(c => c.Name == map.TraktorCommand).Id;
                deviceMap.Settings.Comment = map.Comment;
                deviceMap.Settings.Deck = map.Deck;
                deviceMap.Settings.ControllerType = map.ControllerType;
                deviceMap.Settings.InteractionMode = map.InteractionMode;
                deviceMap.Settings.AutoRepeat = map.AutoRepeat;
                deviceMap.Settings.Invert = map.Invert;
                deviceMap.Settings.SoftTakeover = map.SoftTakeover;
                deviceMap.Settings.RotarySensitivity = map.RotarySensitivity;
                deviceMap.Settings.RotaryAcceleration = map.RotaryAcceleration;
                deviceMap.Settings.LedInvert = map.LedInvert;
                deviceMap.Settings.Resolution = map.Resolution;
            }

            return result;
        }
    }

    public enum SaveModelResult
    {
        Success,
        SuccessWithNonSavedUnknownMappings
    }

    /*

    File
        - Header ID "DIOM", Size = 456880
        - DevicesList
            - Header ID = "DEVS"
            - NumberOfDevices = 1
            - Devices
                - Name                  "Generic MIDI"
                - DeviceTarget          Focus 0, DeckA 1, DeckB 2, DeckC 3, DeckD 4
                - MappingFileComment    "Any comment"
                - DevicePorts
                    - InPortName        "Midi Fighter 3D (Midi Fighter 3D)"
                    - OutPortName       "Midi Fighter 3D (Midi Fighter 3D)"
                - MidiDefinitions
                    - In
                        - MidiDefinition                                (Is MidiNote unique? Are all possible notes listed?)
                            - MidiNoteLength    11 (Probably a const)
                            - MidiNote          "Ch01.CC.000"
                            - Unknown1          7
                            - Unknown2          0
                            - Velocity          127 (Probably a const)
                            - EncoderMode       _7Fh_01h (1) or _3Fh_41h (0)
                            - ControlId         -1 (Possibly a const?)
                    - Out
                        - MidiDefinition (as above except Unknown1 is 8 instead of 7)
                - Mappings
                    - List
                        - Mapping
                            - MidiNoteBindingId     0 upwards
                            - Type                  In 0 or Out 1
                            - TraktorControlId      2248 (maps to the commands.xml list)
                            - Settings
                                - ControllerType    Button 0, FaberOrKnob, Encoder, Led
                                - InteractionMode   Toggle Hold Direct etc
                                - Loads more stuff about each mapping
                    - MidiBindings
                        - Bindings
                            Binding is unique for MidiNote and TraktorCommand. For each combination there can be 2 Mappings, both an in and an out
                            - Id        35 (I think this is the common link between notes and mappings)
                            - MidiNote  Ch03.Note.F#2
    */
}
