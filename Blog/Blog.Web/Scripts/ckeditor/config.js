/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.height = 500;

    config.allowedContent = true;
    config.fillEmptyBlocks = false;

    config.toolbar_PorDefecto =
    [
         { name: 'document', items: ['Source', '-', 'DocProps', "-", 'Templates'] },
        { name: 'tools', items: ['Maximize', 'ShowBlocks', '-', 'About'] },
        { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
       // { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt'] },
        { name: 'insert', items: ['Image', 'Flash', 'Table', /*'HorizontalRule',*/ 'HorizontalLine', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
        { name: 'styles', items: ['Format', 'FontSize'] },
          '/',
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', '-', 'RemoveFormat'] },
        {
            name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote',
            '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
        },
        { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },

        { name: 'colors', items: ['TextColor', 'BGColor'] }
        //,{ name: 'extra', items: ['Timestamp'] }
    ];

    config.toolbar_Basic =
    [
        ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', '-', 'About']
    ];

    config.toolbar_SmallToolbar =
   [
   ['Bold', 'Italic', 'Underline', '-',
    'Link', 'Unlink', '-',
    'Font', 'FontSize', '-', 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-',
    'Undo', 'Redo', '-', 'Source']
   ];
};