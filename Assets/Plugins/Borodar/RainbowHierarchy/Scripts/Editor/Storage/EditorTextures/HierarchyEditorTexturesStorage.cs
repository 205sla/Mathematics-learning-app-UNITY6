﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using StorageHelper = Borodar.RainbowHierarchy.TexturesStorageHelper<Borodar.RainbowHierarchy.HierarchyEditorTexture>;

namespace Borodar.RainbowHierarchy
{
    public static class HierarchyEditorTexturesStorage
    {
        private static readonly Dictionary<HierarchyEditorTexture, Texture2D> EDITOR_TEXTURES;
        private static readonly Dictionary<HierarchyEditorTexture, Lazy<string>> EDITOR_STRINGS;

        //---------------------------------------------------------------------
        // Ctors
        //---------------------------------------------------------------------

        static HierarchyEditorTexturesStorage()
        {
            EDITOR_TEXTURES = new Dictionary<HierarchyEditorTexture, Texture2D>();
            EDITOR_STRINGS = (EditorGUIUtility.isProSkin) ? GetDictPro() : GetDictFree();
        }

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public static Texture2D GetTexture(HierarchyEditorTexture type)
        {
            return StorageHelper.GetTexture(type, FilterMode.Bilinear, EDITOR_STRINGS, EDITOR_TEXTURES);
        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private static Dictionary<HierarchyEditorTexture, Lazy<string>> GetDictPro()
        {
            return new Dictionary<HierarchyEditorTexture, Lazy<string>>
            {
                {HierarchyEditorTexture.IcnEdit,       new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAADOUlEQVRIDdVVXUiTYRR26XSub5tuy23uR13T5SrT0KgMKpOkIm/qIrooKugq7KIuJbqIrg0q6CYJiaKoINCwoqgwRftdmj9T0vk3Y27Ozelsaie+OHvf9/uqWXnhLsZzznnOOe97ft5PUl5enrCUvxVLGfxH7OWfICmeEslT5upOtXGyKJKnIklHLheHZv7sHleJzJowGR3SrEyJWrRhzPcbEFcCk3paGEJUKaSJJNAqZgssAQnBzTcFCeknXGOMKYG83hLQKiNCGltEoFYf6Mw3Bj0TssaPuu4RxeFt7nXmSaHnvo2enIzwzVfmXEOoonDMkDbTM8JV1RYu0FQJs2i7C8bO7HfRnEVINfW2Rx/0pANVImjd8bJ+0rxYfGznADMOiVarFaOcKOsvygmgyIBeD9fiUrs8nESSoOZmGSsvyqTzqclzbX1qtFI92Gr3oYEEgbD0UoOtqVuDylL7+Om9vSr5N9Qg2Jznu9K4GkXqBk1dGocpqFVSp4Omnb/jaCUOBc6D43K40K6Cr+SwgR76XH17LayheAIwPHXqlPKoPTOEjFaX+laTGUUEMGZ5hpBJE1uR+nf6C/fzg9NS5ACgmgxydF7yxJlBMj4PKUmRxIzpsVMXnWOuJEgA/tDDv/uJ+rE3SEpcgFUgEzhMIlvGExhTxYYxcCd9AVNN1qlmLh7q2JJHzZJRM90xqISKM55F2RNHdwyQp4aVLrH6335J/2WTr518b9HGmsZHhBAlNv+IPxUmB3PAmJ6tdMHUo4YHGsVsqd33oDUT9bF5AlVzj7qyeBRtCGDezx3shLnsGeVACcNj08fGDGk8eN0d2zLQUAluvMja7vCKrg9QIehv4vLR/VPSupcWHvP/VJPhC1X7PIs0LxZff5ZNNgDcqSaD3OfhNtl8UMrRCdm9FuPdZpMhPbJK7KEHcvugsqYhd8iXqk+LwBvXOay4SjwS/OHY5xq08N2Ax73drcKJq9rTC68/74D/D98Y8M2BQYAPDgyCN5iMBB5QJeJV3smUT0R0UHYNKxg3RglHcbpVwuhAE0kgjAVFiFMppMWVwO2VM60DkVwLYVzUiPQAbf8FxHWDf8m0/BN8B0f1DQMsgsBGAAAAAElFTkSuQmCC")},
                {HierarchyEditorTexture.IcnFoldoutFirst,  new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAA8UlEQVR4Ae2ZQQrDMBADm9Kf+v9vaOnVh0BAgkx2cltihCyNffGx1npN/t6TN//fuwFIwPAEPALDAfAS/NyAgO/m4djm6ugdUI0XIC4BgJKqFiWgGi9AXAIAJVUtSkA1XoC4BABKqlqUgGq8AHEJAJRUtSgB1XgB4hIAKKlqUQKq8QLExxNwh3eBnZP9nWD/f3U+fWcYT4ABXOXpaevveAecntl0AR6BdKI0PQmgNZb2KwHpRGl6EkBrLO1XAtKJ0vQkgNZY2q8EpBOl6UkArbG0XwlIJ0rTkwBaY2m/EpBOlKYnAbTG0n4lIJ0oTW88AT8muwOwK+8yWQAAAABJRU5ErkJggg==")},
                {HierarchyEditorTexture.IcnFoldoutMiddle, new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAA8klEQVR4Ae2aMQ6EMADDAPHT/v8Nd2OlDgwo0Z1VM8GAiRLDxDnGOH58fJbnn8t19fKq0gFwCwCMVI2oAdV6AXANAIxUjagB1XoBcA0AjFSNqAHVegFwDQCMVI2oAdV6AXANAIxUjagB1XoBcA0AjFSNqAHVegFwDQCMVI2oAdV6AXANAIxUjXhX6e/g6/8C7yjzrsf/DXwFZlF7nm1vwD9+Ax7f2bSn2xtgAWmlaDwNoC2WzqsB6UZpPA2gLZbOqwHpRmk8DaAtls6rAelGaTwNoC2WzqsB6UZpPA2gLZbOqwHpRmk8DaAtls6rAelGabwvvrQDrtVOP8MAAAAASUVORK5CYII=")},
                {HierarchyEditorTexture.IcnFoldoutLast,   new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAA70lEQVR4Ae2aMQ5AQAAEET+9/7+BUnKFQjKxmxsVhbHZHSr7GGP7+bim5+/TNXp5oPQCuAUUjIRG1AC03gK4BhSMhEbUALTeArgGFIyERtQAtN4CuAYUjIRG1AC03gK4BhSMhEbUALTeArgGFIyERtQAtN4CuAYUjIRG1AC03gK4BhSMhEY8Ufo3+Py/wDfKc9fr/wa+Ak9Ra54tb0DCN+D1HaW9XN4AC6AVS+drQPpCdD4NoBtO52tA+kJ0Pg2gG07na0D6QnQ+DaAbTudrQPpCdD4NoBtO52tA+kJ0Pg2gG07na0D6QnQ+DaAbTuffvsQDriqTua4AAAAASUVORK5CYII=")},
                {HierarchyEditorTexture.IcnFoldoutLevels, new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAAgAAAABACAYAAABsv8+/AAAETUlEQVR4Ae3dwa0aQRAEUK9FFE4D0nAUI0EapAFpOA1Iw3FYvtaBw/SpxPu3Fmqp+81fbWkvc6y1fvgjQIAAAQIEvkvg53eta1sCBAgQIEDgv4AA4P+AAAECBAh8oYAA8IWHbmUCBAgQIHAqIPgVM/6Nur08xwLvqNvL37HAn6jby2ss8Iy6vbzHAlnHz3XlIya+Rd1e5vOWz2P7fq9Y4BJ1e5nvu3wfjvbzBWDEp5kAAQIECHQKCACd52ZqAgQIECAwEhAARnyaCRAgQIBAp4AA0HlupiZAgAABAiMBAWDEp5kAAQIECHQKCACd52ZqAgQIECAwEhAARnyaCRAgQIBAp8DhLoDOgzM1AQIECBCYCPgCMNHTS4AAAQIESgUEgNKDMzYBAgQIEJgICAATPb0ECBAgQKBUQAAoPThjEyBAgACBiYAAMNHTS4AAAQIESgUEgNKDMzYBAgQIEJgICAATPb0ECBAgQKBU4FQwd95/nPcjF6zwccRz/PqOur3M+8fzfvL2/a6xwDPq9vIeC2QdP9eVj5j4FnV7mc9bPo/t+71igUvU7WW+7/J9ONrPF4ARn2YCBAgQINApIAB0npupCRAgQIDASEAAGPFpJkCAAAECnQICQOe5mZoAAQIECIwEBIARn2YCBAgQINApIAB0npupCRAgQIDASEAAGPFpJkCAAAECnQLHWqtzclMTIECAAAEC2wK+AGzTaSRAgAABAr0CAkDv2ZmcAAECBAhsCwgA23QaCRAgQIBAr4AA0Ht2JidAgAABAtsCAsA2nUYCBAgQINArIAD0np3JCRAgQIDAtoAAsE2nkQABAgQI9AqcCkbP+4/zfuSCFT6OeI5f31G3l3n/eN5P3r7fNRZ4Rt1e3mOBrOPnuvIRE9+ibi/zecvnsX2/Vyxwibq9zPddvg9H+/kCMOLTTIAAAQIEOgUEgM5zMzUBAgQIEBgJCAAjPs0ECBAgQKBTQADoPDdTEyBAgACBkYAAMOLTTIAAAQIEOgUEgM5zMzUBAgQIEBgJCAAjPs0ECBAgQKBT4FhrdU5uagIECBAgQGBbwBeAbTqNBAgQIECgV0AA6D07kxMgQIAAgW0BAWCbTiMBAgQIEOgVEAB6z87kBAgQIEBgW0AA2KbTSIAAAQIEegUEgN6zMzkBAgQIENgWEAC26TQSIECAAIFegVPB6Hn/cd6PXLDCxxHP8es76vYy7x/P+8nb97vGAs+o28t7LJB1/FxXPmLiW9TtZT5v+Ty27/eKBS5Rt5f5vsv34Wg/XwBGfJoJECBAgECngADQeW6mJkCAAAECIwEBYMSnmQABAgQIdAoIAJ3nZmoCBAgQIDASEABGfJoJECBAgECngADQeW6mJkCAAAECIwEBYMSnmQABAgQIdAoca63OyU1NgAABAgQIbAv4ArBNp5EAAQIECPQKCAC9Z2dyAgQIECCwLSAAbNNpJECAAAECvQL/AG/bKlGRkrW/AAAAAElFTkSuQmCC")},
                {HierarchyEditorTexture.IcnFilter,        new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAADXElEQVR4Ae2ay48NQRTGXbFgYSv+Ent7S6z8Ax4rQoiIK2IhTCQmiFdIvCOETEaIkRkJgkG8MguJCPFKvMJ4M8bvE7OYvnX7dndV9aNun+TL7ep76jy+PlVd3dWNscHpU7pZpnZz8sq9JqCugFYGZnHqEBgPDCfIZzaYJKYhMA+NRZO0wmgsJI350VRMBIR8W5iRhIDrKD2NKgbQfk0O16J5mCrgLkrboooBtHvJ4UY0DxMBf1CS8rGocoXbZ4i9B/yO5mAiQDoioQlGQNXlMQlsAD9MibQjQLqPQBMYO3K+CqILuQloWBsljgB1OAl2GXtW4+QBwhTaSiPBw9BMeg+AOW2tlPOPh4Q1F7yNC69TBajvKFgGPqtREdGwVcyxySuXJARIbxis0UFFZD1xXk4Sa1ICZOsgOK6Dkksf8e1JGmMaAjQENKM+SWq8AL1X+GyCD0l9pyFANjWxaCh8U6NkonG/FtxJE1daAmRbt8Z9aZzkpHsYP0fT+spCgBYXm8HttM486mvFquGpKkglWQiQgxdgBfioRsHyBf8rQaa5KSsBylm3ma1gTI2CZBy/O8C5rP5tCJBPEaBVYlFyBcd60MkstgR8x/NS8DJzBNk7vqfrEvA1u4nkK8E4H3rc1HzQ8qwd18nyP03Eq4Buy1ZiWwETzk9xsHeikcOvbnd6c20trgj4RSRaf9+zjqizAb2n0GLsZ2fVzhquCJCnN2A5eKeGJ/mEXQ23567suyRAMQ2BXh14kt3YPe/StmsCNDn1gAsug/xva4jfjcDpZOuaAMWqp8bVINPKTAYMopJfB0YN/1md8kGAAtJLSC2SXExUWmluB1r0OBdfBCjQnaDPQcQXsbHFgR2jiSQvRY0dQznpswIqwVFNQCUuk8cg86gATYZ6bs+C/R5z/2c6DwJ852BlvybAir4AOtcVEMBFtEqhrgAr+gLoXFdAABfRKoW6AqzoC6BzXQEBXESrFOoKsKIvgM5lr4D7vjme5ttBRvvadr8JtOnqVfIgIE2VPSPbEXAW6EMsp7tA2GuRPAjQdlmc6JO7YXAVnAa3QG6SBwH6lmixISNtcw+AQXAJJP64EV1nkgcB2h06AhaABlC7H+iqPwCFSr0zVCj9JXCeZoYuQbjuQ+h6Av4CudjKndrUilwAAAAASUVORK5CYII=")},
                {HierarchyEditorTexture.IcnDelete,        new Lazy<string>(() => "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")},
                {HierarchyEditorTexture.IcnSettings,      new Lazy<string>(() => "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")},
            };
        }

        private static Dictionary<HierarchyEditorTexture, Lazy<string>> GetDictFree()
        {
            return new Dictionary<HierarchyEditorTexture, Lazy<string>>
            {
                {HierarchyEditorTexture.IcnEdit,      new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAChElEQVRIDeVVzetpURR9Pz2Sz/JdGPEYCQMxMCJk6C9VZhiJkpFkJJRC+Q4hH8Vb75067Xvu9bq/Xr/B6xlo7bXX2vvec84+96NSqXz7yp/mK4v/qv3vN/iuZokej0e5XL7f71ys1WpLpZJOp+PMO6BqiY7HI62OWmh5OBzeFaW82gbUwzC6ykk5o9Dgcrksl0sqXa/XNGR4s9lQcrVawUgZhhX2oNlsoqLJZAoEAg6Ho9/vwyx3DofD/X4fiUR2u91oNDqdTna7vVgsCsoPYdDG43G73RZE6sNUKhUMBqleskTYum63S9OfxbALx0HSoNfrXa/Xd0VtNtuP3z+Ad5rb7YYiNCvZg9lsRnMc6/X6ZDLp9/s5M51OO52O4tOgSCKR4ErJG+RyOewqz3GQTqdpdfAIQXIBB9hnFOEhgKSB0WjM5/OhUIgqvF6vx+OhDMMgkaI81q9QKOD4UVLSAAmNRoPTSRVOp5OGFAspGGGnAmAxBvV6vQSRylDRKDZ4Pp8YBVpRcYyZQEjBCDv1AksaYBqr1SpGlIrm8/lisaAMwyCRojzmGfbz+UxJSYNarbbdbmma4VarhXNJeYQgKcMw7PV6nfKSOcDhGwwGNM0wznuj0cB84RSCQRXcP3IZY3w+H01JGkSj0clkgmmkCo5R9A91mQwjiSLcAiBZInyh4vE4TX8Ww46PHXVJGiCBu5CtA+YlFotlMhnhsFOzy+XKZrN4ZDZcUAozBLF4XYPCdwPHCWZeC9eOcLSQCofD9M7BN8psNhsMBu5iQHwDsBDR6mAULyiBdLvd8urwKjRgnem/xWKhIcOKpFymqoHVahW2DqHKBgp7IH+Kv2FUvcH/3eAn+b75RLaqSOAAAAAASUVORK5CYII=")},
                {HierarchyEditorTexture.IcnFoldoutFirst,  new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAA8UlEQVR4Ae2ZQQrDMBADm9Kf+v9vaOnVh0BAgkx2cltihCyNffGx1npN/t6TN//fuwFIwPAEPALDAfAS/NyAgO/m4djm6ugdUI0XIC4BgJKqFiWgGi9AXAIAJVUtSkA1XoC4BABKqlqUgGq8AHEJAJRUtSgB1XgB4hIAKKlqUQKq8QLExxNwh3eBnZP9nWD/f3U+fWcYT4ABXOXpaevveAecntl0AR6BdKI0PQmgNZb2KwHpRGl6EkBrLO1XAtKJ0vQkgNZY2q8EpBOl6UkArbG0XwlIJ0rTkwBaY2m/EpBOlKYnAbTG0n4lIJ0oTW88AT8muwOwK+8yWQAAAABJRU5ErkJggg==")},
                {HierarchyEditorTexture.IcnFoldoutMiddle, new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAA8klEQVR4Ae2aMQ6EMADDAPHT/v8Nd2OlDgwo0Z1VM8GAiRLDxDnGOH58fJbnn8t19fKq0gFwCwCMVI2oAdV6AXANAIxUjagB1XoBcA0AjFSNqAHVegFwDQCMVI2oAdV6AXANAIxUjagB1XoBcA0AjFSNqAHVegFwDQCMVI2oAdV6AXANAIxUjXhX6e/g6/8C7yjzrsf/DXwFZlF7nm1vwD9+Ax7f2bSn2xtgAWmlaDwNoC2WzqsB6UZpPA2gLZbOqwHpRmk8DaAtls6rAelGaTwNoC2WzqsB6UZpPA2gLZbOqwHpRmk8DaAtls6rAelGabwvvrQDrtVOP8MAAAAASUVORK5CYII=")},
                {HierarchyEditorTexture.IcnFoldoutLast,   new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAA70lEQVR4Ae2aMQ5AQAAEET+9/7+BUnKFQjKxmxsVhbHZHSr7GGP7+bim5+/TNXp5oPQCuAUUjIRG1AC03gK4BhSMhEbUALTeArgGFIyERtQAtN4CuAYUjIRG1AC03gK4BhSMhEbUALTeArgGFIyERtQAtN4CuAYUjIRG1AC03gK4BhSMhEY8Ufo3+Py/wDfKc9fr/wa+Ak9Ra54tb0DCN+D1HaW9XN4AC6AVS+drQPpCdD4NoBtO52tA+kJ0Pg2gG07na0D6QnQ+DaAbTudrQPpCdD4NoBtO52tA+kJ0Pg2gG07na0D6QnQ+DaAbTuffvsQDriqTua4AAAAASUVORK5CYII=")},
                {HierarchyEditorTexture.IcnFoldoutLevels, new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAAgAAAABACAYAAABsv8+/AAAETUlEQVR4Ae3dwa0aQRAEUK9FFE4D0nAUI0EapAFpOA1Iw3FYvtaBw/SpxPu3Fmqp+81fbWkvc6y1fvgjQIAAAQIEvkvg53eta1sCBAgQIEDgv4AA4P+AAAECBAh8oYAA8IWHbmUCBAgQIHAqIPgVM/6Nur08xwLvqNvL37HAn6jby2ss8Iy6vbzHAlnHz3XlIya+Rd1e5vOWz2P7fq9Y4BJ1e5nvu3wfjvbzBWDEp5kAAQIECHQKCACd52ZqAgQIECAwEhAARnyaCRAgQIBAp4AA0HlupiZAgAABAiMBAWDEp5kAAQIECHQKCACd52ZqAgQIECAwEhAARnyaCRAgQIBAp8DhLoDOgzM1AQIECBCYCPgCMNHTS4AAAQIESgUEgNKDMzYBAgQIEJgICAATPb0ECBAgQKBUQAAoPThjEyBAgACBiYAAMNHTS4AAAQIESgUEgNKDMzYBAgQIEJgICAATPb0ECBAgQKBU4FQwd95/nPcjF6zwccRz/PqOur3M+8fzfvL2/a6xwDPq9vIeC2QdP9eVj5j4FnV7mc9bPo/t+71igUvU7WW+7/J9ONrPF4ARn2YCBAgQINApIAB0npupCRAgQIDASEAAGPFpJkCAAAECnQICQOe5mZoAAQIECIwEBIARn2YCBAgQINApIAB0npupCRAgQIDASEAAGPFpJkCAAAECnQLHWqtzclMTIECAAAEC2wK+AGzTaSRAgAABAr0CAkDv2ZmcAAECBAhsCwgA23QaCRAgQIBAr4AA0Ht2JidAgAABAtsCAsA2nUYCBAgQINArIAD0np3JCRAgQIDAtoAAsE2nkQABAgQI9AqcCkbP+4/zfuSCFT6OeI5f31G3l3n/eN5P3r7fNRZ4Rt1e3mOBrOPnuvIRE9+ibi/zecvnsX2/Vyxwibq9zPddvg9H+/kCMOLTTIAAAQIEOgUEgM5zMzUBAgQIEBgJCAAjPs0ECBAgQKBTQADoPDdTEyBAgACBkYAAMOLTTIAAAQIEOgUEgM5zMzUBAgQIEBgJCAAjPs0ECBAgQKBT4FhrdU5uagIECBAgQGBbwBeAbTqNBAgQIECgV0AA6D07kxMgQIAAgW0BAWCbTiMBAgQIEOgVEAB6z87kBAgQIEBgW0AA2KbTSIAAAQIEegUEgN6zMzkBAgQIENgWEAC26TQSIECAAIFegVPB6Hn/cd6PXLDCxxHP8es76vYy7x/P+8nb97vGAs+o28t7LJB1/FxXPmLiW9TtZT5v+Ty27/eKBS5Rt5f5vsv34Wg/XwBGfJoJECBAgECngADQeW6mJkCAAAECIwEBYMSnmQABAgQIdAoIAJ3nZmoCBAgQIDASEABGfJoJECBAgECngADQeW6mJkCAAAECIwEBYMSnmQABAgQIdAoca63OyU1NgAABAgQIbAv4ArBNp5EAAQIECPQKCAC9Z2dyAgQIECCwLSAAbNNpJECAAAECvQL/AG/bKlGRkrW/AAAAAElFTkSuQmCC")},
                {HierarchyEditorTexture.IcnFilter,        new Lazy<string>(() => "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAADYUlEQVR4Ae2byWoVQRSGveJCX0B8KJfRlS/gsFIURdQr4kJUBIOzouCMKEpQFCNGUDFGxYksBBHFCZzQOE/x+8Es0qmb7pq6763bB35yq/vUGf4+VV1d3WkM7pw6pZulu7PnytcEdHP5K3dTBczk+EEwmhiOk88sME5MBMxGY944rTQac0mjJ5uKiYDpWaWE2jOyuZgIuIHS06xiAu3X5HA9m4eJgLsobckqJtDuJYfBbB4mAv6iJOWjWeUObp8m9s3gdzYHEwHSEQlNMAw6XR6TwFrww5RIKwKk+wg0gbEjxztBdCHXAw1ro0xGgDqcADuMPTvj4H7CFFpKHgHquBrcbGmhfU88JLTleeEVIWAEI4vA5zxjbXRew1Yxv82LqQgBsjEEVuQZa6Pza4jlSpF4ihIgWwfAMf1oc+kjvt1FY7QhQENAM+qTosYr0HuFzyb4UNS3DQGyqYlFQ+GbGm0mGvcrwR2buGwJkG3dGvfaOClJ9xB+jtj6ciFAi4sN4Lats4j6WrFqeKoKrMSFADl4AZaAj2pULF/wvxQ4zU2uBChn3WY2gT9qVCSj+N0Gzrn69yFAPkVAv6vzAP2uYkMPOs7iS8B3PC8EL50jcO/4nq4LwFd3E+ZNUVt7etzUfDDhWdvWkIW+JuJlQLdlL/GtgDHnJ/mxZ6xRwl/d7rRz7S2hCPhFJFp/3/OOKN+A9im0GPuZr5qvEYoAeXoDFoN3akSST9jVcHseyn5IAhTTAOjVj0iyC7vnQ9oOTYAmJ20+XggZ5H9bA/xdB4JOtqEJUKx6atROjNPKTAYMopJfBUYM57wOxSBAAWkTUoukEBOVVppbgRY9wSUWAQp0O+gLEPFFbGwMYMdoYprxaLiDPeFMxbEUswLiRBzYak1AYEI7zlwZFaDJUM/tLtgXm9EyCIidg5f9mgAv+hLoXFdAAhfRK4W6ArzoS6BzXQEJXESvFOoK8KIvgc51BSRwEb1SqCvAi74EOrd7BdyPzXHsTVHX+PXaXV+n6qVrVCmDAJsqe0a2w+AM0IdYQd8CYW+ClEGAXpdNJvrkbghcA6fALVCalEGAviWab8hIr7n7wWVwCRT+uBHdYFIGAXo7dBjMAQ2g9lmgq/4AVCqN+n+HK+W/euc2M3T10UaIoOsJ+AeUd8Y/ZB3GmQAAAABJRU5ErkJggg==")},
                {HierarchyEditorTexture.IcnDelete,        new Lazy<string>(() => "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")},
                {HierarchyEditorTexture.IcnSettings,      new Lazy<string>(() => "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")},
            };
        }
    }
}